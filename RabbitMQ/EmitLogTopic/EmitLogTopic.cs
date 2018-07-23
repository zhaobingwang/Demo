using System;
using RabbitMQ.Client;
using System.Text;
using System.Linq;

namespace EmitLogTopic
{
    class EmitLogTopic
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1" };
            using (var conncetion = factory.CreateConnection())
            using (var channel = conncetion.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");
                var routeKey = (args.Length > 0) ? args[0] : "anonymous.info";
                var message = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "topic_logs", routingKey: routeKey, basicProperties: null, body: body);
                Console.WriteLine($" [x] Sent {routeKey}:{message}");
            }
        }
    }
}
