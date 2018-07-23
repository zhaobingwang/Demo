using System;
using System.Linq;
using SERedis.Common;
using StackExchange.Redis;

namespace SERedis.SetType
{
    class SetType
    {
        private static IDatabase redis = null;
        static void Main(string[] args)
        {
            redis = RedisManager.Instance.GetDatabase(1);
            //Set();
            //Set2();
            //Get();
            GetCombine();
        }

        private static void Get()
        {
            var result = redis.SetMembers(Constant.T_SET_KEY1);
            result.ToList().ForEach(x =>
            {
                Console.WriteLine(x);
            });
            Console.WriteLine("--------------------");
        }

        private static void Set()
        {
            redis.SetAdd(Constant.T_SET_KEY1, "hi");
            redis.SetAdd(Constant.T_SET_KEY1, "hello");
            redis.SetAdd(Constant.T_SET_KEY1, "嗨");
        }

        private static void Set2()
        {
            redis.SetAdd(Constant.T_SET_KEY2, "hi");
            redis.SetAdd(Constant.T_SET_KEY2, "你好呀");
        }

        private static void GetCombine()
        {
            // SetOperation.Intersect 交集
            // SetOperation.Intersect 差集
            // SetOperation.Intersect 并集
            var result = redis.SetCombine(SetOperation.Union, Constant.T_SET_KEY1, Constant.T_SET_KEY2);
            foreach (var item in result.OrderBy(x => x).ToList())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------------");
        }
    }
}
