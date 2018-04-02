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
        public static RedisHelper redis;
        static void Main(string[] args)
        {
            redis = new RedisHelper(0);

            // 1.string
            RedisStringSet("TestStringKey", "TestStringValue", TimeSpan.FromSeconds(30)).GetAwaiter().GetResult();
        }


        public static async Task RedisStringSet(string key, string value, TimeSpan? timeSpan = null)
        {
            Console.WriteLine("开始设置string值");

            await redis.StringSetAsync<string>("TestStringKey", "TestStringValue", TimeSpan.FromSeconds(30));
            Console.WriteLine("完成string值得设置");
            while (true)
            {
                if (redis.KeyExists(nameof(key)))
                {
                    Console.WriteLine($"{DateTime.Now} # key:{key},value:{await redis.StringGetAsync<string>(nameof(key))}");
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now} # key:{key},value:{await redis.StringGetAsync<string>(nameof(key))} 已过期");
                    break;
                }
            }
        }
        public static async Task InsertAsync(int count, string prefix)
        {

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
