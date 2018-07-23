using System;
using SERedis.Common;

namespace SERedis.StringType
{
    class StringType
    {
        static void Main(string[] args)
        {
            var redis = RedisManager.Instance.GetDatabase(1);
            var success = redis.StringSet(Constant.T_STRING_KEY, "123", TimeSpan.FromSeconds(30));
            Console.WriteLine(success);

            var result = redis.StringGet(Constant.T_STRING_KEY);
            Console.WriteLine(result);
        }
    }
}
