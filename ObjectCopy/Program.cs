using System;
using System.Reflection;

namespace ObjectCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            User u1 = new User();
            u1.Name = "张三";
            u1.Age = 20;
            Console.WriteLine($"u1.name:{u1.Name} - u1.age:{u1.Age}");

            User u3 = DeepCopyByReflect(u1);

            User u2 = u1;
            u2.Name = "李四";
            u2.Age = 30;
            Console.WriteLine($"u2.name:{u2.Name} - u2.age:{u2.Age}");
            Console.WriteLine($"u3.name:{u3.Name} - u3.age:{u3.Age}");
        }

        public static T DeepCopyByReflect<T>(T obj)
        {
            if (obj is string || obj.GetType().IsValueType)
                return obj;
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj)));
                }
                catch
                {
                }
            }
            return (T)retval;
        }
    }
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
