using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AysncDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();   //模拟异步main
        }

        /// <summary>
        /// 模拟异步main
        /// </summary>
        /// <returns></returns>
        static async Task MainAsync()
        {
            int contentLength = await AccessTheWebAsync();
            Console.WriteLine($"Length of downloaded string:{contentLength}");
        }


        static async Task<int> AccessTheWebAsync()
        {
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");
            DoIndependentWork();
            string urlContents = await getStringTask;
            return urlContents.Length;
        }
        static void DoIndependentWork()
        {
            System.Console.WriteLine("Working......\r\n");
        }
    }
}
