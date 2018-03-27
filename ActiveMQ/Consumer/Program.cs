using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
                using (IConnection connection = factory.CreateConnection())
                {
                    connection.ClientId = "firstQueueListener";
                    connection.Start();
                    using (ISession session = connection.CreateSession())
                    {
                        IMessageConsumer consumer = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("test"), "testKey='testValue'");
                        consumer.Listener += new MessageListener(consumer_listener);
                        Console.Read();
                    }
                    connection.Stop();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        private static void consumer_listener(IMessage message)
        {
            try
            {
                ITextMessage msg = (ITextMessage)message;
                Console.WriteLine($"Receive:{msg.Text}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}
