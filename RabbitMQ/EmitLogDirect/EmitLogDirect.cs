using System;
using RabbitMQ.Client;
using System.Text;
using System.Linq;

namespace EmitLogDirect
{
    class EmitLogDirect
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1" };
            using (var conncetion = factory.CreateConnection())
            using (var channel = conncetion.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");
                var severity = args.Length > 0 ? args[0] : "info";
                var message = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello World!";

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "direct_logs", routingKey: severity, basicProperties: null, body: body);
                Console.WriteLine($" [x] Sent {severity}:{message}");
            }
            Console.WriteLine(" Press [enter] to exit");
            Console.ReadLine();
        }
    }
}
