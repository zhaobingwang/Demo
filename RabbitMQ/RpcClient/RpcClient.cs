using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Collections.Concurrent;

namespace RpcClient
{

    public class Rpc
    {
        static void Main(string[] args)
        {
            var rpcClient = new RpcClient();
            Console.WriteLine(" [x] Requesting fib(10)");
            var response = rpcClient.Call("10");
            Console.WriteLine($" [.]Got '{response}'");
            rpcClient.Close();
        }
    }
    public class RpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public RpcClient()
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }

        public string Call(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: "rpc_queue", basicProperties: props, body: messageBytes);
            channel.BasicConsume(consumer: consumer, queue: replyQueueName, autoAck: false);
            return respQueue.Take();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
