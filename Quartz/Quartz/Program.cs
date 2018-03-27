using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Quartz
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcuteJob();
            System.Threading.Thread.Sleep(10000);
        }
        public static async void ExcuteJob()
        {
            // 1.创建一个调度器
            // 1.1调度器
            IScheduler scheduler;
            // 1.2调度工厂
            ISchedulerFactory factory;
            // 1.3创建一个调度器
            factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // 2.创建一个任务对象
            IJobDetail job = JobBuilder.Create<TimeJob>().WithIdentity("job1", "group1").Build();

            // 3.创建一个触发器
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithCronSchedule("0/2 * * * * ?")
                .Build();

            // 4.将任务与触发器添加到调度器中
            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();
        }
    }



    public class TimeJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"CurrentTime:{DateTime.Now}");
                //System.IO.File.AppendAllText(@"c:\Quartz.txt", DateTime.Now + Environment.NewLine);
            });


            return t;
        }
    }
}
