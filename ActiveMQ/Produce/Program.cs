using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace Produce
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.创建ConnectoryFactory连接工厂
            IConnectionFactory factory = new ConnectionFactory(("tcp://localhost:61616"));
            
            // 2.创建Connection连接
            using (IConnection connection=factory.CreateConnection())
            {
                connection.Start(); //默认关闭？
                // 3.创建session？
                using (ISession session=connection.CreateSession())
                {
                    // 4.创建Destination
                    IDestination destination = new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("test");

                    // 5.创建MessageProducer
                    IMessageProducer producer = session.CreateProducer();

                    // 6.设置持久化方式
                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

                    //7.定义消息对象，并发送
                    ITextMessage message = session.CreateTextMessage();// producer.CreateTextMessage();
                    message.Text = "test";
                    message.Properties.SetString("testKey", "testValue");
                    //producer.Send(destination, message);
                    producer.Send(destination, message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.FromDays(30));

                    Console.WriteLine("send success");
                    Console.Read();
                }
                if (connection!=null)
                {
                    connection.Close();
                }
            }
            
        }
    }
}
