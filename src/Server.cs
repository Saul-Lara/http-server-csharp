using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 4221);
server.Start();
Socket socket = server.AcceptSocket(); // wait for client

// Read request
byte[] httpRequest = new byte[1024];
socket.Receive(httpRequest);

string requestData = Encoding.UTF8.GetString(httpRequest);

string[] httpRequestParts = requestData.Split("\r\n");
string requestLine = httpRequestParts[0];
string[] requestLineParts = requestLine.Split(" ");
var (httpMethod, requestTarget, httpVersion) = (requestLineParts[0], requestLineParts[1], requestLineParts[2]);
string urlPath = requestTarget.IndexOfAny("/".ToCharArray(), 1) == -1 ? requestTarget : requestTarget.Substring(0, requestTarget.IndexOfAny("/".ToCharArray(), 1));

Dictionary<string, string> responses = new Dictionary<string, string>
{
  {"/", "HTTP/1.1 200 OK\r\n\r\n" },
  {"/echo", "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: %length%\r\n\r\n%message%" },
  {"/user-agent" , "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: %length%\r\n\r\n%message%"}
};

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

}
else
{
  //Sends a 400 response string to the connected client.
  socket.Send(Encoding.UTF8.GetBytes("HTTP/1.1 404 Not Found\r\n\r\n"));
}