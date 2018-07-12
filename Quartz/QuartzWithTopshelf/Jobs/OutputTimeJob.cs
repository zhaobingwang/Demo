using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzWithTopshelf.Jobs
{
    public class OutputTimeJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"CurrentTime:{DateTime.Now}");
            });
            return t;
        }
    }
}
