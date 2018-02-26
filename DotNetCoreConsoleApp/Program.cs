using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DotNetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("命令行配置：");
            var settings = new Dictionary<string, string>
            {
                { "name","no8"},
                { "age","20"}
            };
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .AddCommandLine(args);
            var configuration = builder.Build();
            System.Console.WriteLine($"name:{configuration["name"]}");
            System.Console.WriteLine($"name:{configuration["age"]}");

            System.Console.WriteLine("Json配置：");
            var builderJson = new ConfigurationBuilder()
                .AddJsonFile("class.json");
            var configurationJson = builderJson.Build();
            System.Console.WriteLine($"ClassNo:{configurationJson["ClassNo"]}");
            System.Console.WriteLine($"ClassNo:{configurationJson["ClassDesc"]}");
            System.Console.WriteLine("Studdents:");
            System.Console.WriteLine($"{configurationJson["Students:0:name"]}-{configurationJson["Students:0:age"]}");
            System.Console.WriteLine($"{configurationJson["Students:1:name"]}-{configurationJson["Students:1:age"]}");
            System.Console.WriteLine($"{configurationJson["Students:2:name"]}-{configurationJson["Students:2:age"]}");
        }
    }
}
