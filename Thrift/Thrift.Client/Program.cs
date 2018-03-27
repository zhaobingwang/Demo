using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Transport;
using Thrift.Protocol;

namespace Thrift.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TTransport transport = new TSocket("localhost", 8088);  // 设置调用地址为本地，端口为8088
            transport.Open();
            TProtocol protocol = new TBinaryProtocol(transport);    // 设置传输协议为 TBinaryProtocol
            MathService.Client client = new MathService.Client(protocol);
            //Console.WriteLine(client.add(1, 2));    //调用服务的 add() 方法
            OPNum opNum = new OPNum();
            opNum.Num1 = 10;
            opNum.Num2 = 20;
            Console.WriteLine(client.Compute(EnumOP.Div, opNum));
            transport.Close();
            Console.ReadKey();
        }
    }
    
}
