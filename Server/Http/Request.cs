namespace MonsterCardTrading.Http_Server
{
    public class Request
    {
        public string Method { get; private set; }
        public string Path { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        public string Body { get; private set; }

        public Request(StreamReader reader)
        {
            Headers = new Dictionary<string, string>();
            ParseRequest(reader);
        }

        private void ParseRequest(StreamReader reader)
        {
            string[] requestLine = reader.ReadLine()?.Split(' ') ?? throw new InvalidOperationException("Invalid request line.");
            Method = requestLine[0];
            Path = requestLine[1];

            string line;
            while (!string.IsNullOrWhiteSpace(line = reader.ReadLine()))
            {
                var headerParts = line.Split(new[] { ':' }, 2);
                if (headerParts.Length == 2)
                {
                    Headers[headerParts[0].Trim()] = headerParts[1].Trim();
                }
            }

            if (Headers.ContainsKey("Content-Length"))
            {
                int contentLength = int.Parse(Headers["Content-Length"]);
                char[] bodyChars = new char[contentLength];
                reader.Read(bodyChars, 0, contentLength);
                Body = new string(bodyChars);
            }
            else
            {
                Body = string.Empty;
            }
        }
    }
}
