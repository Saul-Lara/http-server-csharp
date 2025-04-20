[![progress-banner](https://backend.codecrafters.io/progress/http-server/f97c8cdc-b906-4f7f-a5a0-ceebdff27553)](https://app.codecrafters.io/users/codecrafters-bot?r=2qF)

# 🖧 HTTP Server (C#)

![GitHub repo size](https://img.shields.io/github/repo-size/Saul-Lara/http-server-csharp?style=for-the-badge)
![GitHub license](https://img.shields.io/github/license/Saul-Lara/http-server-csharp?style=for-the-badge)
![GitHub last commit](https://img.shields.io/github/last-commit/Saul-Lara/http-server-csharp?color=green&style=for-the-badge)
[![emoji-log](https://img.shields.io/badge/emoji--log-blue?style=for-the-badge&logo=rocket&logoColor=white&labelColor=413855&color=8679A2)](https://github.com/ahmadawais/Emoji-Log/)

This project is a basic HTTP server built with C# as part of the ["Build Your Own HTTP server"](https://app.codecrafters.io/courses/http-server/overview) challenge. It demonstrates the implementation of core HTTP features, including routing, file serving, header handling, and more.

> [!NOTE]
> 🧪 Try the challenge yourself on [codecrafters.io](https://codecrafters.io)!

## :pushpin: Features

:white_check_mark: HTTP/1.1 protocol support  
:white_check_mark: Support for multiple endpoints (`/`, `/echo`, `/user-agent`, `/files`)  
:white_check_mark: Dynamic headers: `Content-Length`, `Content-Type`, `Content-Encoding`  
:white_check_mark: Gzip compression (when requested)  
:white_check_mark: File serving and uploads with configurable root directory  
:white_check_mark: MIME type detection  
:white_check_mark: Support for concurrent connections  
:white_check_mark: Basic error responses (`404 Not Found`, `500 Internal Server Error`, etc.)

## :rocket: Built With

- **Programming Language**: C# (.NET 9.0)
- **Automation**: PowerShell (`your_program.ps1`) for local build/run

## :hammer: Manual Build & Run

```bash
dotnet build --configuration Release
./bin/Release/net9.0/codecrafters-http-server.exe [--directory <path>]
```

Options:

- `--directory <path>`: Root directory for file handling (optional).
  Defaults to system temp path based on the OS (`C:\tmp\` or `/tmp/`) if not provided.

## 🔧 Build & Run (PowerShell)

```bash
.\your_program.ps1 [-directory <path>]
```

Options:

- `-directory <path>`: Root directory for file handling (optional).

## :books: Endpoints

| Method | Endpoint            | Description                                |
| ------ | ------------------- | ------------------------------------------ |
| GET    | `/`                 | Returns a 200 OK response                  |
| GET    | `/echo/<string>`    | Echoes the message back to the client      |
| GET    | `/user-agent`       | Returns the client’s User-Agent string     |
| GET    | `/files/<filename>` | Serves a file from the server directory    |
| POST   | `/files/<filename>` | Uploads/updates a file in server directory |

> Gzip compression is applied to `/echo` if the client includes `Accept-Encoding: gzip`.

## :memo: Technical Details

- Listens on port `4221`
- Raw socket usage via `System.Net.Sockets`
- Requests are parsed from byte stream to extract method, path, headers, and body
- MIME type detection
- Gzip compression (when requested)
- Concurrent requests handled using the Task-based asynchronous programming

## :label: Response Headers

- `Content-Type`: Appropriate MIME type for response
- `Content-Length`: Size of response body
- `Content-Encoding: gzip`: When compression is supported and used

## :warning: Error Handling

- `404 Not Found`: For non-existent resources
- `500 Internal Server Error`: For server-side failures

## :notebook: Blog Explanation

Check out my post series for a detailed explanation of how this project was developed:

[Bit by Bit: Building an HTTP Server in C#](https://saul-lara.hashnode.dev/series/bit-by-bit-csharp-server)

## :thought_balloon: Conclusion

This project is a demonstration of how a HTTP server can be implemented. During the development, I learnt about some key concepts such as TCP connections, HTTP headers, HTTP verbs, handling multiple connections, and file serving.

---
