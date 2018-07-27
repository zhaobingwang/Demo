using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonDemo
{
    class JsonDemo
    {
        static void Main(string[] args)
        {
            // 基本使用
            Product product = new Product();
            product.Name = "Watermelon";
            product.ExpireDate = new DateTime(2019, 8, 31);
            product.Price = 29.99m;
            product.Sizes = new string[] { "Small", "Medium", "Large" };
            string output = JsonConvert.SerializeObject(product);
            Console.WriteLine(output);

            // List<T> => Json
            List<Product> list = new List<Product> {
                new Product(){
                    Name="Watermelon",
                    ExpireDate=new DateTime(2019, 8, 31),
                    Price=29.99m,
                    Sizes=new string[]{ "Small", "Medium", "Large" }
                },
                new Product(){
                    Name="Apple",
                    ExpireDate=new DateTime(2019, 9, 30),
                    Price=1.99m,
                    Sizes=new string[]{ "Small", "Medium", "Large" }
                },
            };
            string outputList2Json = JsonConvert.SerializeObject(list);
            Console.WriteLine($"{nameof(outputList2Json)}:");
            Console.WriteLine(outputList2Json);

            string jsons = "[{'Name':'Watermelon','ExpireDate':'2019-08-31T00:00:00','Price':29.99,'Sizes':['Small','Medium','Large']},{'Name':'Apple','ExpireDate':'2019-09-30T00:00:00','Price':1.99,'Sizes':['Small','Medium','Large']}]";
            var products=JsonConvert.DeserializeObject<List<Product>>(jsons);
            products.ForEach(foo =>
            {
                Console.WriteLine(foo.Name);
            });
        }
    }

    class Product
    {
        public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Price { get; set; }
        public string[] Sizes { get; set; }
    }
}
