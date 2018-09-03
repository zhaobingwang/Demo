using System;
using Topshelf;
using MassTransit;
using NLog;
using MassTransitCommon;
using System.Threading.Tasks;

namespace MassTransitConsumerService
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static int Main(string[] args)
        {
            return (int)HostFactory.Run(cfg => cfg.Service(x => new EventConsumerService()));
        }

        private class EventConsumerService : ServiceControl
        {
            IBusControl _bus;
            public bool Start(HostControl hostControl)
            {
                logger.Info($"MassTransitConsumerService服务开启。。。");
                _bus = ConfigureBus();
                _bus.Start();
                return true;
            }

            public bool Stop(HostControl hostControl)
            {
                logger.Info($"MassTransitConsumerService服务关闭。。。");
                _bus?.Stop(TimeSpan.FromSeconds(30));
                return true;
            }
            IBusControl ConfigureBus()
            {
                return Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://172.18.34.189"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint(host, "test_queue", e =>
                    {
                        e.Handler<MyMessage>(context =>
                        {
                            Task t = new Task(() =>
                            {
                                //Console.WriteLine($"Received:{context.Message.Value}");
                                logger.Info($"Received:{context.Message.Value}");
                            });
                            t.Start();
                            return t;
                        });
                    });
                });
            }
        }
    }
}
