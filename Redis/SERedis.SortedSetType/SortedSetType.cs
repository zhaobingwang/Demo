using System;
using StackExchange.Redis;
using SERedis.Common;

namespace SERedis.SortedSetType
{
    class SortedSetType
    {
        private static IDatabase redis = null;
        static void Main(string[] args)
        {
            redis = RedisManager.Instance.GetDatabase(1);
            //Set();
            Get();
            Increment();
            Get();
        }
        private static void Get()
        {
            RedisValue[] result = redis.SortedSetRangeByRank(Constant.T_SORTEDSET_KEY, 0, 2, Order.Descending);
            for (int i = 0; i < result.Length; i++)
            {
                var value = redis.SortedSetScore(Constant.T_SORTEDSET_KEY, result[i]);
                Console.WriteLine($"{result[i]}-{value}");
                // Output:
                // t3-33
                // t2-22
                // t1-11
            }
        }
        private static void Set()
        {
            redis.SortedSetAdd(Constant.T_SORTEDSET_KEY, "t1", 11);
            redis.SortedSetAdd(Constant.T_SORTEDSET_KEY, "t3", 33);
            redis.SortedSetAdd(Constant.T_SORTEDSET_KEY, "t2", 22);
            Console.WriteLine("ok");
        }

        /// <summary>
        /// 累加
        /// </summary>
        private static void Increment()
        {
            redis.SortedSetIncrement(Constant.T_SORTEDSET_KEY, "t1", 5);    // t1存在，则累加
            redis.SortedSetIncrement(Constant.T_SORTEDSET_KEY, "t4", 5);    // t4不存在，则新增
            Console.WriteLine("Increment Ok");
        }
    }
}
