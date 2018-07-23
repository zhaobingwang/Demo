using System;
using System.Linq;
using SERedis.Common;
using StackExchange.Redis;

namespace SERedis.ListType
{
    class ListType
    {
        private static IDatabase redis = null;
        static void Main(string[] args)
        {
            redis = RedisManager.Instance.GetDatabase(1);
            Set();
            Get();
        }

        static void Set()
        {
            for (int i = 0; i < 10; i++)
            {
                redis.ListRightPush(Constant.T_LIST_KEY, i + 1);
            }
        }
        static void Get()
        {
            var result = redis.ListRange(Constant.T_LIST_KEY).ToList();
            result.ForEach(x =>
            {
                Console.WriteLine(x);
            });
        }
    }
}
