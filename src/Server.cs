using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Runtime.InteropServices;

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

Dictionary<string, string> responses = new Dictionary<string, string>
{
  {"/", "HTTP/1.1 200 OK\r\n\r\n" },
  {"/echo", "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: %length%\r\n\r\n%message%" },
  {"/user-agent" , "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: %length%\r\n\r\n%message%"},
  {"/files", "HTTP/1.1 200 OK\r\nContent-Type: %mime_type%\r\nContent-Length: %length%\r\n\r\n%message%"}
};

TcpListener server = new TcpListener(IPAddress.Any, 4221);
server.Start();

while (true)
{
  Socket socket = server.AcceptSocket(); // wait for client
  _ = Task.Run(() => handleRequest(socket, responses));
}

void handleRequest(Socket socket, Dictionary<string, string> responses)
{
  Console.WriteLine($"Connection from {socket.RemoteEndPoint} has been established");

  // Read request
  byte[] httpRequest = new byte[1024];
  socket.Receive(httpRequest);

  string requestData = Encoding.UTF8.GetString(httpRequest);

  string[] httpRequestParts = requestData.Split("\r\n");
  string requestLine = httpRequestParts[0];
  string[] requestLineParts = requestLine.Split(" ");
  var (httpMethod, requestTarget, httpVersion) = (requestLineParts[0], requestLineParts[1], requestLineParts[2]);
  string urlPath = requestTarget.IndexOfAny("/".ToCharArray(), 1) == -1 ? requestTarget : requestTarget.Substring(0, requestTarget.IndexOfAny("/".ToCharArray(), 1));

  if (responses.ContainsKey(urlPath))
  {
    //Sends a response string to the connected client.
    if (urlPath == "/")
    {
      socket.Send(Encoding.UTF8.GetBytes(responses[urlPath]));
    }
    else if (urlPath == "/echo")
    {
      string data = requestTarget.Replace("/echo/", "");
      string responseTemplate = responses[urlPath];
      responseTemplate = responseTemplate.Replace("%length%", data.Length.ToString()); // Replacing content-length
      responseTemplate = responseTemplate.Replace("%message%", data);                  // Replacing response body
      socket.Send(Encoding.UTF8.GetBytes(responseTemplate));
    }
    else if (urlPath == "/user-agent")
    {
      string userAgentData = httpRequestParts.FirstOrDefault(item => item.StartsWith("user-agent") || item.StartsWith("User-Agent")) ?? "User-Agent: null";
      string userAgentValue = userAgentData.Split(":")[1].Trim();

      string responseTemplate = responses[urlPath];
      responseTemplate = responseTemplate.Replace("%length%", userAgentValue.Length.ToString()); // Replacing content-length
      responseTemplate = responseTemplate.Replace("%message%", userAgentValue);                  // Replacing response body
      socket.Send(Encoding.UTF8.GetBytes(responseTemplate));
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
          string fileContent = File.ReadAllText(filePath);  // Open the file to read from.

          string responseTemplate = responses[urlPath];
          responseTemplate = responseTemplate.Replace("%mime_type%", mimeType);                     // Replacing content-type
          responseTemplate = responseTemplate.Replace("%length%", fileContent.Length.ToString());   // Replacing content-length
          responseTemplate = responseTemplate.Replace("%message%", fileContent);                    // Replacing response body
          socket.Send(Encoding.UTF8.GetBytes(responseTemplate));
        }
        else
        {
          socket.Send(Encoding.UTF8.GetBytes("HTTP/1.1 404 Not Found\r\n\r\n"));
        }
      }
      else if (httpMethod == "POST")
      {
        string contentLengthHeader = httpRequestParts.Where(item => item.StartsWith("Content-Length") || item.StartsWith("content-length")).First();
        int contentLengthValue = int.Parse(contentLengthHeader.Split(":")[1].Trim()); // Obtain the Content-Lenght value

        string requestBody = httpRequestParts.Last();               // Obtain the request body
        requestBody = requestBody.Substring(0, contentLengthValue); // Avoid any additional request data
        File.WriteAllText(filePath, requestBody);                   // Create and Write the request body in the file

        socket.Send(Encoding.UTF8.GetBytes("HTTP/1.1 201 Created\r\n\r\n"));
      }
    }
  }
  else
  {
    //Sends a 404 response string to the connected client.
    socket.Send(Encoding.UTF8.GetBytes("HTTP/1.1 404 Not Found\r\n\r\n"));
  }

  //Closes the socket to free up system resources
  socket.Close();
}