using MassTransit;
using MassTransitCommon;
using System;
using System.Threading.Tasks;

namespace MassTransitPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var busControl = ConfigureBus();

            // Important! The bus must be started before using it!
            busControl.Start();

            do
            {
                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;
                var endPoint = busControl.GetSendEndpoint(new Uri("rabbitmq://172.18.34.189/test_queue")).Result;
                endPoint.Send<MyMessage>(new MyMessage { Value = value });
                //busControl.Publish<MyMessage>(new
                //{
                //    Value = value
                //});
            }
            while (true);

            busControl.Stop();
        }
        static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://172.18.34.189"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }
        //public async Task SendOrder(ISendEndpointProvider sendEndpointProvider)
        //{
        //    var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("rabbitmq://172.18.34.189/test_queue"));

        //    await endpoint.Send(new MyMessage { Value="111"});
        //}

    }
}
