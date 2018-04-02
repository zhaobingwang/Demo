using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    class Program
    {
        static void Main(string[] args)
        {
            //var redis = new RedisHelper(0);
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 100000; i++)
            //{
            //    redis.StringSet($"USER:{Guid.NewGuid()}", $"用户{i + 1}");
            //}
            //sw.Stop();;
            //Console.WriteLine($"同步插入100k条记录，耗时：{sw.ElapsedMilliseconds} ms.");

            Console.WriteLine("异步插入：");
            Task t1 = InsertAsync(20000, "A");
            Task t2 = InsertAsync(20000, "B");
            Task t3 = InsertAsync(20000, "C");
            Task t4 = InsertAsync(20000, "D");
            Task t5 = InsertAsync(20000, "E");
            Console.Read();
        }

        public static async Task InsertAsync(int count,string prefix)
        {
            var redis = new RedisHelper(0);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                await redis.StringSetAsync($"USER:{Guid.NewGuid()}", $"{prefix}{i + 1}");
            }
            sw.Stop();
            Console.WriteLine($"{prefix}:成功插入{count}条记录，耗时：{sw.ElapsedMilliseconds} ms.");
        }
    }


    class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
