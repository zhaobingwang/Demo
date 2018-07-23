using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace SERedis.Common
{
    public class RedisManager
    {
        private RedisManager()
        {

        }
        private static ConnectionMultiplexer instance;
        private static readonly object locker = new object();

        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null || !instance.IsConnected)
                        {
                            instance = ConnectionMultiplexer.Connect("127.0.0.1");
                        }
                    }
                }
                return instance;
            }
        }
    }
}
