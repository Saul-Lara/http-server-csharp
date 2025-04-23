using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Runtime.InteropServices;
using System.IO.Compression;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

string[] arguments = Environment.GetCommandLineArgs();

// If no additional arguments are passed, use a default temporary directory path based on the operating system.
string tmpDirectoryPath = arguments.Length == 1 ? RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"C:\tmp\" : "/tmp/" : arguments[2];

if (!Directory.Exists(tmpDirectoryPath))
{
  Directory.CreateDirectory(tmpDirectoryPath);
  Console.WriteLine("The temporary directory was created successfully");
}

HashSet<string> validEndpoints = new HashSet<string> { "/", "/echo", "/user-agent", "/files" };
HashSet<string> validCompressions = new HashSet<string> { "gzip" };

TcpListener server = new TcpListener(IPAddress.Any, 4221);
server.Start();

while (true)
{
  Socket socket = server.AcceptSocket(); // wait for client
  _ = Task.Run(() => handleRequest(socket));
}

void handleRequest(Socket socket)
{
  Console.WriteLine($"Connection from {socket.RemoteEndPoint} has been established");

  while (socket.Connected)
  {
    // Read request
    byte[] httpRequest = new byte[1024];
    int bytesReceived = socket.Receive(httpRequest);
    if (bytesReceived == 0) break; // client disconnected


    string requestData = Encoding.UTF8.GetString(httpRequest);

    string[] httpRequestParts = requestData.Split("\r\n");
    string requestLine = httpRequestParts[0];
    string[] requestLineParts = requestLine.Split(" ");
    var (httpMethod, requestTarget, httpVersion) = (requestLineParts[0], requestLineParts[1], requestLineParts[2]);
    string urlPath = requestTarget.IndexOfAny("/".ToCharArray(), 1) == -1 ? requestTarget : requestTarget.Substring(0, requestTarget.IndexOfAny("/".ToCharArray(), 1));

    string connectionData = httpRequestParts.FirstOrDefault(item => item.StartsWith("Connection", StringComparison.OrdinalIgnoreCase)) ?? "";

    // Initialize empty headers and body for each new request
    Dictionary<string, string> headers = new Dictionary<string, string>();
    headers["Content-Length"] = "0";  // Sets Content-Length with 0 by default
    byte[] body = Array.Empty<byte>();
    bool shouldClose = false;

    if (connectionData.Contains(':')) // Check for `Connection: close` header
    {
      string connectionValue = connectionData.Split(":", 2)[1].Trim().ToLower();
      if (connectionValue == "close")
      {
        headers["Connection"] = "close"; // Appends Connection header if present
        shouldClose = true;
      }
    }

    if (validEndpoints.Contains(urlPath))
    {
      //Sends a response string to the connected client.
      if (urlPath == "/")
      {
        socket.Send(generateHttpResponse(httpVersion, 200, "OK", headers, body));
      }
      else if (urlPath == "/echo")
      {
        string data = requestTarget.Replace("/echo/", "");
        byte[] responseBody = Encoding.UTF8.GetBytes(data); // Encode the response body using UTF-8 by default

        string encodingData = httpRequestParts.FirstOrDefault(item => item.StartsWith("Accept-Encoding", StringComparison.OrdinalIgnoreCase)) ?? "";

        if (!string.IsNullOrEmpty(encodingData) && encodingData.Contains(':'))
        {
          string encodingValues = encodingData.Split(":", 2)[1].Trim().ToLower();

          // Iterate through each encoding type in the Accept-Encoding header
          foreach (var compressionType in encodingValues.Split(",").Select(values => values.Trim()))
          {
            if (validCompressions.Contains(compressionType)) // Check if the compression type is supported
            {
              if (compressionType == "gzip") // If gzip is supported and requested, compress the body and update headers
              {
                headers["Content-Encoding"] = $"{compressionType}";
                responseBody = handleGzipCompression(data);
                break;
              }
            }
          }
        }

        headers["Content-Type"] = "text/plain";                 // Sets content-type
        headers["Content-Length"] = $"{responseBody.Length}";   // Sets Content-Length based on response body

        socket.Send(generateHttpResponse(httpVersion, 200, "OK", headers, responseBody));
      }
      else if (urlPath == "/user-agent")
      {
        string userAgentData = httpRequestParts.FirstOrDefault(item => item.StartsWith("user-agent") || item.StartsWith("User-Agent")) ?? "User-Agent: null";
        string userAgentValue = userAgentData.Split(":")[1].Trim();

        headers["Content-Type"] = "text/plain";                 // Sets content-type
        headers["Content-Length"] = $"{userAgentValue.Length}"; // Sets Content-Length based on response body
        body = Encoding.UTF8.GetBytes(userAgentValue);          // Get bytes of the response body

        socket.Send(generateHttpResponse(httpVersion, 200, "OK", headers, body));
      }
      else if (urlPath == "/files")
      {
        string fileNameRequested = requestTarget.Replace("/files/", "");
        string filePath = String.Concat(tmpDirectoryPath, fileNameRequested);

        if (httpMethod == "GET")
        {
          if (File.Exists(filePath) && fileNameRequested.Length > 0)
          {
            string fileExtension = Path.GetExtension(filePath);
            string mimeType = MimeTypes.GetMimeTypeOrDefault(fileExtension, "application/octet-stream");
            string fileContent = File.ReadAllText(filePath);     // Open the file to read from.

            headers["Content-Type"] = mimeType;                  // Sets content-type
            headers["Content-Length"] = $"{fileContent.Length}"; // Sets Content-Length based on response body
            body = Encoding.UTF8.GetBytes(fileContent);          // Get bytes of the response body

            socket.Send(generateHttpResponse(httpVersion, 200, "OK", headers, body));
          }
          else
          {
            socket.Send(generateHttpResponse(httpVersion, 404, "Not Found", headers, body));
          }
        }
        else if (httpMethod == "POST")
        {
          string contentLengthHeader = httpRequestParts.Where(item => item.StartsWith("Content-Length") || item.StartsWith("content-length")).First();
          int contentLengthValue = int.Parse(contentLengthHeader.Split(":")[1].Trim()); // Obtain the Content-Lenght value

          string requestBody = httpRequestParts.Last();               // Obtain the request body
          requestBody = requestBody.Substring(0, contentLengthValue); // Avoid any additional request data
          File.WriteAllText(filePath, requestBody);                   // Create and Write the request body in the file

          socket.Send(generateHttpResponse(httpVersion, 201, "Created", headers, body));
        }
      }
    }
    else
    {
      //Sends a 404 response string to the connected client.
      socket.Send(generateHttpResponse(httpVersion, 404, "Not Found", headers, body));
    }

    //Closes the socket to free up system resources
    if (shouldClose)
    {
      Console.WriteLine($"Connection from {socket.RemoteEndPoint} has been closed by the client.");
      socket.Close();
      break;
    }
  }
}

byte[] handleGzipCompression(string originalData)
{
  byte[] dataToCompress = Encoding.UTF8.GetBytes(originalData);
  using (var outputStream = new MemoryStream())
  {
    using (var compressor = new GZipStream(outputStream, CompressionMode.Compress))
    {
      compressor.Write(dataToCompress, 0, dataToCompress.Length);
    }

    return outputStream.ToArray();
  }
}

byte[] generateHttpResponse(string httpVersion, int statusCode, string statusMessage, Dictionary<string, string> headers,
                            byte[] body)
{
  string response = $"{httpVersion} {statusCode} {statusMessage}\r\n";

  foreach ((string headerName, string value) in headers)
  {
    response += $"{headerName}: {value}\r\n";
  }

  response += "\r\n";

  byte[] responseBytes = Encoding.UTF8.GetBytes(response);

  return [.. responseBytes, .. body];
}