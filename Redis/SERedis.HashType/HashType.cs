using StackExchange.Redis;
using System;
using SERedis.Common;

namespace SERedis.HashType
{
    class HashType
    {
        private static IDatabase redis = null;
        static void Main(string[] args)
        {
            redis = RedisManager.Instance.GetDatabase(1);
            Set();
            Get();
        }

        private static void Get()
        {
            var name = redis.HashGet(Constant.T_HASH_KEY, "name");
            var age = redis.HashGet(Constant.T_HASH_KEY, "age");
            var address = redis.HashGet(Constant.T_HASH_KEY, "address");
            Console.WriteLine($"{name}\t{age}\t{address}");
        }
        private static void Set()
        {
            redis.HashSet(Constant.T_HASH_KEY, "name", "张三");
            redis.HashSet(Constant.T_HASH_KEY, "age", "20");
            redis.HashSet(Constant.T_HASH_KEY, "address", "北京");
            Console.WriteLine("ok");
        }
    }
}
