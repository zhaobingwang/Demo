using System;
using MassTransit;

namespace MassTransitDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://172.18.34.189"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                sbc.ReceiveEndpoint(host, "test_queue", endpoint =>
                {
                    endpoint.Handler<MyMessage>(async context =>
                    {
                        await Console.Out.WriteAsync($"Received:{context.Message.Value}");
                    });
                });
            });


            bus.Start();
            bus.Publish(new MyMessage { Value = "Hello,World." });
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
            bus.Stop();
        }
    }
    class MyMessage
    {
        public string Value { get; set; }
    }
}
