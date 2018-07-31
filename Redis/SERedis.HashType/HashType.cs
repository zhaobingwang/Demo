using StackExchange.Redis;
using System;
using SERedis.Common;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;
using System.Diagnostics;

namespace SERedis.HashType
{
    class HashType
    {
        private static IDatabase redis = null;
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            redis = RedisManager.Instance.GetDatabase(1);
            //Set();
            //ObjectToHashEntryList();
            //Get();
            sw.Start();
            SetObjBySerialize();
            sw.Stop();
            Console.WriteLine($" #耗时：{sw.ElapsedMilliseconds} ms");
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

        public static void SetObjBySerialize()
        {
            var users = GetFakeUsers();
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var user in users)
            {
                string json = JsonConvert.SerializeObject(user);
                listHashEntry.Add(new HashEntry($"{user.Id}", json));
            }
            redis.HashSet(Constant.T_HASH_KEY, listHashEntry.ToArray());
        }





        [Obsolete("作废")]
        public static void ObjectToHashEntryList()
        {
            var user = new User()
            {
                Id = 1004,
                Name = "测试",
                Address = "杭州",
                RegTime = new DateTime(2018, 7, 30),
                IsDelete = true
            };
            List<HashEntry> list = new List<HashEntry>();
            foreach (PropertyInfo u in user.GetType().GetProperties())
            {
                var name = u.Name.ToString();
                var val = u.GetValue(user);
                list.Add(new HashEntry(name, val.ToString()));
            }
            redis.HashSet(Constant.T_HASH_KEY, list.ToArray());
        }


        private static List<User> GetFakeUsers()
        {
            return new List<User>() {
                new User{
                    Id=1001,
                    Name="张三",
                    Address="北京",
                    RegTime=new DateTime(2018,7,31),
                    IsDelete=false
                },
                new User{
                    Id=1002,
                    Name="李四",
                    Address="上海",
                    RegTime=new DateTime(2018,7,30),
                    IsDelete=true
                },
                                new User{
                    Id=1003,
                    Name="王五",
                    Address="杭州",
                    RegTime=new DateTime(2018,7,29),
                    IsDelete=false
                },
            };
        }
    }
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime RegTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
