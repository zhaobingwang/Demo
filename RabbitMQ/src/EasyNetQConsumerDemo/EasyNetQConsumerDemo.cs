using EasyNetQ;
using EasyNetQMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetQConsumerDemo
{
    class EasyNetQConsumerDemo
    {
        static void Main(string[] args)
        {
            using (var bus=RabbitHutch.CreateBus("host=127.0.0.1"))
            {
                bus.Subscribe<TextMessage>("test", HandleTextMessage);
                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }
        static void HandleTextMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Message);
            Console.ResetColor();
        }
    }
}
