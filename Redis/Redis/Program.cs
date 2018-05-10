using System;
using System.Collections.Generic;
using System.Configuration;
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
            //RedisStringSet("TestStringKey", "TestStringValue", TimeSpan.FromSeconds(60)).GetAwaiter().GetResult();

            // 2.Hash
            RedisHashSet().GetAwaiter().GetResult();
        }


        /// <summary>
        /// String设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static async Task RedisStringSet(string key, string value, TimeSpan? timeSpan = null)
        {
            Console.WriteLine("开始设置string值");

            await redis.StringSetAsync<string>("TestStringKey", "TestStringValue", timeSpan);
            Console.WriteLine("完成string值得设置");
            while (true)
            {
                if (redis.KeyExists(key))
                {
                    Console.WriteLine($"{DateTime.Now} # key:{key},value:{await redis.StringGetAsync<string>(key)}");
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now} # key:{key},value:{await redis.StringGetAsync<string>(key)} 已过期");
                    break;
                }
            }
        }

        public static async Task RedisHashSet()
        {
            Console.WriteLine("开始设置Hash值");
            string redisKey = "TestHashKey";
            string redisKey2 = "TestHashKey2";
            //IEnumerable<StackExchange.Redis.HashEntry> hashEntries = new {
            //    key
            //};

            await redis.HashSetAsync(redisKey, "Name", "张三");
            await redis.HashSetAsync(redisKey, "Gender", "男");
            await redis.HashSetAsync(redisKey, "Age", "22");

            //StackExchange.Redis.RedisValue redisValue = new StackExchange.Redis.RedisValue() {
            //    Key = "1"
            //};
            //StackExchange.Redis.HashEntry hashEntry=new StackExchange.Redis.HashEntry();
            //hashEntry
            //await redis.HashSetAsync(redisKey2,new User(){Name="李四",Id="1"});

            Console.WriteLine("HashKey查询结果:");
            var hashKey = await redis.HashKeysAsync(redisKey);
            foreach (var key in hashKey)
            {
                Console.WriteLine(key);
            }

            var hashValue = await redis.HashValuesAsync(redisKey);
            Console.WriteLine("HashValue查询结果：");
            foreach (var value in hashValue)
            {
                Console.WriteLine(value);
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
