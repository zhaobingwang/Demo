using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace QuartzWithTopshelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<QuartzService>(s =>
                {
                    s.ConstructUsing(name => new QuartzService());

                    // 开启和关闭  必选项
                    s.WhenStarted(q => q.Start());
                    s.WhenStopped(q => q.Stop());

                    //暂停和继续  选填
                    s.WhenPaused(q => q.Pause());
                    s.WhenContinued(q => q.Continue());

                });
                x.RunAsLocalSystem();
                x.SetDescription("TestExampleService");
                x.SetDisplayName("TestExample");
                x.SetServiceName("Test");
            });
        }
    }
}
