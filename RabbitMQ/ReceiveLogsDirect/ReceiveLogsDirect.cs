using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ReceiveLogsDirect
{
    class ReceiveLogsDirect
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");
                var queueName = channel.QueueDeclare().QueueName;

                if (args.Length < 1)
                {
                    Console.WriteLine($"Usage:{Environment.GetCommandLineArgs()[0]} [info] [warning] [error]");
                    Console.WriteLine(" Press [entet] to exit.");
                    Console.ReadLine();
                    Environment.ExitCode = 1;
                    return;
                }

                foreach (var severity in args)
                {
                    channel.QueueBind(queue: queueName, exchange: "direct_logs", routingKey: severity);
                }
                Console.WriteLine(" [*] Waiting for message.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine($" [x] Received {routingKey}:{message}");
                };
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}
