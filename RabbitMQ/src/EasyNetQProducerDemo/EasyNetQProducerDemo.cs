using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQMessages;

namespace EasyNetQProducerDemo
{
    class EasyNetQProducerDemo
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=127.0.0.1"))
            {
                var input = "";
                Console.WriteLine("Enter a message.'Quit' to quit.");
                while ((input = Console.ReadLine()).ToLower() != "quit")
                {
                    bus.Publish(new TextMessage
                    {
                        Message = input
                    });
                }
            }
        }
    }
}
