namespace MonsterCardTrading.Http_Server
{
    public class Response
    {
        private StreamWriter _writer;

        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string ContentType { get; set; }
        public string Body { get; set; }

        public Response(StreamWriter writer)
        {
            _writer = writer;
            StatusCode = 200;
            StatusDescription = "OK";
            ContentType = "text/html; charset=utf-8";
            Body = "";
        }

        public void Send()
        {
            _writer.WriteLine($"HTTP/1.1 {StatusCode} {StatusDescription}");
            _writer.WriteLine($"Content-Type: {ContentType}");
            _writer.WriteLine($"Content-Length: {Body.Length}");
            _writer.WriteLine();
            _writer.WriteLine(Body);
            _writer.Flush();
        }

        public void SendText(string content)
        {
            ContentType = "text/plain";
            Body = content;
            Send();
        }

        public void SendJson(string json)
        {
            ContentType = "application/json";
            Body = json;
            Send();
        }
    }
}
