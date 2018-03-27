using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thrift.Server
{
    public class MathServiceImpl : MathService.Iface
    {
        public int add(int a, int b)
        {
            Console.WriteLine($"Called add {a}+{b}={a + b}");
            return a + b;
        }

        public double Compute(EnumOP op, OPNum num)
        {
            double num1 = num.Num1;
            double num2 = num.Num2;
            switch (op)
            {
                case EnumOP.Add:
                    Console.WriteLine($"Called {op} {num1}+{num2}={num1 + num2}");
                    break;
                case EnumOP.Sub:
                    Console.WriteLine($"Called {op} {num1}-{num2}={num1 - num2}");
                    break;
                case EnumOP.Mult:
                    Console.WriteLine($"Called {op} {num1}*{num2}={num1 * num2}");
                    break;
                case EnumOP.Div:
                    Console.WriteLine($"Called {op} {num1}/{num2}={num1 / num2}");
                    break;
                default:
                    Console.WriteLine($"undefined operation.");
                    break;
            }
            return 1;
        }
    }
}
