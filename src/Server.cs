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

Dictionary<string, string> responses = new Dictionary<string, string>
{
  {"/", "HTTP/1.1 200 OK\r\n\r\n" }
};

if (responses.ContainsKey(requestTarget))
{
    //Sends a response string to the connected client.
    socket.Send(Encoding.UTF8.GetBytes(responses[requestTarget]));
}
else
{
    //Sends a 400 response string to the connected client.
    socket.Send(Encoding.UTF8.GetBytes("HTTP/1.1 404 Not Found\r\n\r\n"));
}