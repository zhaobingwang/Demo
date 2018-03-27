using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Server;
using Thrift.Transport;
//using static Thrift.Protocol.TBinaryProtocol;
using Thrift.Protocol;

namespace Thrift.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //MathServiceImpl handler = new MathServiceImpl();
                //MathService.Processor processor = new MathService.Processor(handler);
                //TServerTransport serverTransport = new TServerSocket(8088);
                //TServer server = new TSimpleServer(processor, serverTransport);
                //Console.WriteLine("Starting the server...");
                //server.Serve();

                TServerSocket serverTransport = new TServerSocket(8088);    // 设置服务端口为8088
                //Factory factory = new Factory();    // 设置协议工厂为 TBinaryProtocol
                TBinaryProtocol.Factory factory = new TBinaryProtocol.Factory();
                TTransportFactory transportFactory = new TTransportFactory();
                TProcessor processor = new MathService.Processor(new MathServiceImpl());
                TServer server = new TThreadPoolServer(processor, serverTransport, transportFactory, factory);
                Console.WriteLine($"Starting the server");
                server.Serve();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("Done.");
        }
    }
}
