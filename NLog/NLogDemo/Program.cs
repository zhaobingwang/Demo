using System;
using NLog;

namespace NLogDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Trace("Trace Message");
            logger.Debug("Debug Message");
            logger.Info("Info Message");
            logger.Error("Error Message");
            logger.Fatal("Fatal Message");
            Console.WriteLine("Hello World!");
        }
    }
}
