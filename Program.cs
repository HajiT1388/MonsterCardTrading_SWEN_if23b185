class Program
{
    static void Main(string[] args)
    {
        var server = new MonsterCardTrading.Http_Server.HttpServer("127.0.0.1", 10001);
        server.Start();
    }
}




