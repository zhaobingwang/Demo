using System;
using System.Collections.Generic;
using RedisDemo.Common;

namespace RedisDemo.ListType
{
    class ListType
    {
        public static RedisHelper redis;
        static void Main(string[] args)
        {
            redis = new RedisHelper(0);
            //Set();
            Get();
        }
        static void Set()
        {
            redis.ListRightPush(Constant.T_LIST_KEY, "张三");
            redis.ListRightPush(Constant.T_LIST_KEY, "李四");
            redis.ListRightPush(Constant.T_LIST_KEY, "王五");
            for (int i = 0; i < 100; i++)
            {
                redis.ListLeftPush(Constant.T_LIST_KEY, $"用户{i + 1}");
                redis.li
            }

            Console.WriteLine("ok");
        }

        // todo:
        static void Get()
        {

        }

        public static User GetUser(int id = 0)
        {
            List<User> list = new List<User>();
            list.Add(new User()
            {
                Id = 1,
                Name = "张三",
                Address = "北京",
                RegTime = DateTime.Now
            });
            list.Add(new User()
            {
                Id = 2,
                Name = "李四",
                Address = "上海",
                RegTime = DateTime.Now
            });
            list.Add(new User()
            {
                Id = 3,
                Name = "王五",
                Address = "杭州",
                RegTime = DateTime.Now
            });
            return list.Find(x => x.Id == id);
        }
    }

    [Serializable]
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime RegTime { get; set; }
    }
}
