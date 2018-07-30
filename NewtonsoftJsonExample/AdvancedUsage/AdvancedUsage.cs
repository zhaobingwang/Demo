using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AdvancedUsage
{
    // https://www.cnblogs.com/yanweidie/p/4605212.html
    class AdvancedUsage
    {
        static void Main(string[] args)
        {
            Person person = new Person { Age = 0, Name = "张三", Sex = "男", IsMarry = false, Birthday = new DateTime(2002, 1, 1) };
            Person1 person1 = new Person1 { Age = 8, Name = "张三", Sex = "男", IsMarry = false, Birthday = new DateTime(2010, 1, 1) };
            Person2 person2 = new Person2 { Age = 8, Name = "张三", Sex = "男", IsMarry = false, Birthday = new DateTime(2010, 1, 1) };
            var json = "{'Age':10,'Name':'张三','Sex':'男','IsMarry':false,'Birthday':'2000-01-01T00:00:00'}";

            #region 1.忽略某些属性
            // OptIn:默认情况下,所有的成员不会被序列化,类中的成员只有标有特性JsonProperty的才会被序列化,当类的成员很多,但客户端仅仅需要一部分数据时,推荐使用。
            // OptOut:默认值,类中所有公有成员会被序列化,如果不想被序列化,可以用特性JsonIgnore。
            var json1_1 = JsonConvert.SerializeObject(person1);
            Console.WriteLine($"忽略某些属性-OptIn：\n{json1_1}\n");
            var json1_2 = JsonConvert.SerializeObject(person2);
            Console.WriteLine($"忽略某些属性-OptOut：\n{json1_2}]\n");
            #endregion

            #region 2.默认值处理
            Person p2 = new Person();
            string json2_1 = JsonConvert.SerializeObject(p2, Formatting.Indented);
            Console.WriteLine($"默认值-未处理：\n{json2_1}\n");

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            var json2_2 = JsonConvert.SerializeObject(p2, Formatting.Indented, jsetting);
            Console.WriteLine($"默认值-忽略：\n{json2_2}\n");
            #endregion

            #region 3.空值处理
            Person p3_1 = new Person { Age = 0, Name = "张三", Sex = null, IsMarry = false, Birthday = new DateTime(2002, 1, 1) };
            var json3_1 = JsonConvert.SerializeObject(p3_1, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            Console.WriteLine($"空值处理第一种方式:\n{json3_1}");

            Person3_2 p3_2 = new Person3_2 { Age = 0, Name = "张三", Sex = null, IsMarry = false, Birthday = new DateTime(2002, 1, 1) };
            var json3_2 = JsonConvert.SerializeObject(p3_2);
            Console.WriteLine($"空值处理第二种方式:\n{json3_2}");
            #endregion

            #region 4.支持非公共成员 ??????
            //Person4 p4 = new Person4 { Age = 0, Name = "张三", Sex = "男", IsMarry = false, Birthday = new DateTime(2002, 1, 1) };
            #endregion

            #region 5.日期处理
            Person5 p5 = new Person5 { Age = 0, Name = "张三", Sex = null, IsMarry = false, Birthday = new DateTime(2018, 7, 30) };
            var json5 = JsonConvert.SerializeObject(p5);
            // 未处理时的日期格式："2018-07-30T00:00:00"
            Console.WriteLine($"\n日期处理：{json5}");
            #endregion
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Person1
    {
        public int Age { get; set; }

        [JsonProperty]
        public string Name { get; set; }
        public string Sex { get; set; }
        public bool IsMarry { get; set; }
        public DateTime Birthday { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class Person2
    {
        [JsonIgnore]
        public int Age { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public bool IsMarry { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public bool IsMarry { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class Person3_2
    {
        public int Age { get; set; }
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Sex { get; set; }
        public bool IsMarry { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class Person4
    {
        [JsonProperty]
        private int Age { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public bool IsMarry { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class Person5
    {
        [JsonProperty]
        public int Age { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public bool IsMarry { get; set; }

        [JsonConverter(typeof(ChinaDatetimeConverter))]
        public DateTime Birthday { get; set; }
    }
    public class ChinaDatetimeConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter.WriteJson(writer, value, serializer);
        }
    }
}
