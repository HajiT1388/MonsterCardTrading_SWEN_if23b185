using MonsterCardTrading.Http_Server;
using MonsterCardTrading.Models;
using System.Text.Json;


namespace MonsterCardTrading_SWEN_if23b185.Server.Http.Endpoint
{
    public class UserEndpoint
    {
        private Dictionary<string, string> _users;
        private Dictionary<string, string> _sessions;

        public UserEndpoint(Dictionary<string, string> users, Dictionary<string, string> sessions)
        {
            _users = users;
            _sessions = sessions;
        }

        public void HandleUserRequest(Request request, Response response)
        {
            if (request.Method == "POST" && request.Path == "/users")
            {
                var requestData = JsonSerializer.Deserialize<User>(request.Body);

                if (_users.ContainsKey(requestData.Username))
                {
                    response.StatusCode = 400;
                    response.StatusDescription = "Ungültige Anfrage";
                    response.SendText("<html><body><h1>Benutzer existiert bereits!</h1></body></html>");
                }
                else
                {
                    _users.Add(requestData.Username, requestData.Password);
                    response.StatusCode = 201;
                    response.StatusDescription = "Erstellt";
                    response.SendText("<html><body><h1>Benutzer erfolgreich registriert!</h1></body></html>");
                }
            }
            else if (request.Method == "POST" && request.Path == "/sessions")
            {
                var requestData = JsonSerializer.Deserialize<User>(request.Body);

                if (_users.TryGetValue(requestData.Username, out var storedPassword) && storedPassword == requestData.Password)
                {
                    var token = GenerateToken();
                    _sessions[requestData.Username] = token;
                    response.SendText($"<html><body><h1>Token: {token}</h1></body></html>");
                }
                else
                {
                    response.StatusCode = 401;
                    response.StatusDescription = "Nicht autorisiert";
                    response.SendText("<html><body><h1>Ungültige Anmeldedaten!</h1></body></html>");
                }
            }
            else
            {
                response.StatusCode = 404;
                response.StatusDescription = "Nicht gefunden";
                response.SendText("<html><body><h1>Endpoint nicht gefunden</h1></body></html>");
            }
        }

        private string GenerateToken()
        {
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }

}
