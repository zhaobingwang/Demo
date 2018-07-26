using System;
using Newtonsoft.Json;

namespace JsonDemo
{
    class JsonDemo
    {
        static void Main(string[] args)
        {
            Product product = new Product();
            product.Name = "Watermelon";
            product.ExpireDate = new DateTime(2019, 8, 31);
            product.Price = 29.99m;
            product.Sizes = new string[] { "Small", "Medium", "Large" };
            string output = JsonConvert.SerializeObject(product);
            Console.WriteLine(output);
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
