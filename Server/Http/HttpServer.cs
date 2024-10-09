using System.Net;
using System.Net.Sockets;
using MonsterCardTrading_SWEN_if23b185.Server.Http.Endpoint;

namespace MonsterCardTrading.Http_Server
{
    public class HttpServer
    {
        private TcpListener _httpServer;
        private Dictionary<string, string> _users;
        private Dictionary<string, string> _sessions;
        private UserEndpoint _userEndpoint;

        public HttpServer(string ipAddress, int port)
        {
            _httpServer = new TcpListener(IPAddress.Parse(ipAddress), port);
            _users = new Dictionary<string, string>();
            _sessions = new Dictionary<string, string>();
            _userEndpoint = new UserEndpoint(_users, _sessions); 
        }

        public void Start()
        {
            _httpServer.Start();
            Console.WriteLine("HTTP Server wird gestartet");

            while (true)
            {
                var clientSocket = _httpServer.AcceptTcpClient();
                using var writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
                using var reader = new StreamReader(clientSocket.GetStream());

                var request = new Request(reader);
                var response = new Response(writer);

                HandleRequest(request, response);
            }
        }

        private void HandleRequest(Request request, Response response)
        {
            if (request.Path.StartsWith("/users") || request.Path.StartsWith("/sessions"))
            {
                _userEndpoint.HandleUserRequest(request, response);
            }
            else
            {
                response.StatusCode = 404;
                response.StatusDescription = "Nicht gefunden";
                response.SendText("Endpoint nicht gefunden");
            }
        }
    }
}
