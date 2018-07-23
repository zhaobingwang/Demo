using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ReceiveLogsTopic
{
    class ReceiveLogsTopic
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1" };
            using (var conncetion = factory.CreateConnection())
            using (var channel = conncetion.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");
                var queueName = channel.QueueDeclare().QueueName;

                if (args.Length < 1)
                {
                    Console.WriteLine($"Usage:{Environment.GetCommandLineArgs()[0]} [binding_key...]");
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                    Environment.ExitCode = 1;
                    return;
                }

                foreach (var bindingKey in args)
                {
                    channel.QueueBind(queue: queueName, exchange: "topic_logs", routingKey: bindingKey);
                }

                Console.WriteLine(" [*] Waiting for message.To exit press CTRL+C");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine($" [x] Received {routingKey}:{message}");
                };
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
