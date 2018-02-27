using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int v1 = 0;
            int v2 = v1;
            v2 = 1;
            Console.WriteLine($"v1:{v1}  |  v2:{v2}");

            User u1 = new User();
            u1.Name = "张三";
            u1.Age = 20;

            User u2 = u1;
            u2.Name = "李四";
            u2.Age = 30;
            Console.WriteLine($"u1.name:{u1.Name} | u1.age:{u1.Age}");
        }
    }

    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
