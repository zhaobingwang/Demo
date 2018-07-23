using System;
using RedisDemo.Common;

namespace RedisDemo.StringType
{
    class StrignType
    {
        public static RedisHelper redis;
        private static string value = "123";
        static void Main(string[] args)
        {
            redis = new RedisHelper(0);
            Set();
            Get();
        }
        static void Set()
        {

            var success = redis.StringSet(Constant.T_STRING_KEY, value, TimeSpan.FromSeconds(30));
            if (success)
            {
                Console.WriteLine("添加成功");
            }
        }
        static void Get()
        {
            var result = redis.StringGet(Constant.T_STRING_KEY);
            Console.WriteLine($"Get {Constant.T_STRING_KEY}:{result}");
        }
    }
}
