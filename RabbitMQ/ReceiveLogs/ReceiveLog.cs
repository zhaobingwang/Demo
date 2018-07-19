using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ReceiveLogs
{
    class ReceiveLog
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1" };
            using (var conncetion = factory.CreateConnection())
            using (var channel = conncetion.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                    exchange: "logs",
                    routingKey: "");
                Console.WriteLine($" [*] Waiting for logs");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" [x] {message}");
                };
                channel.BasicConsume(queue: queueName,
                    autoAck: true,
                    consumer: consumer);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
