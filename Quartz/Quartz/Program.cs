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

            /*
             * cron expressions,由7段构成：秒 分 时 日 月 星期 年（可选）
             * "-" 表示范围： MON-WED 表示周一到周三
             * "," 表示列举： MON,WED 表示周一和周三
             * "*" 表示"每"： 每天，每月，每周，每年等
             * "/" 表示增量： 在0分开始，0/15每15分钟；从3分钟开始，3/20 每20分钟；  #前面示例表示处于分钟时段里
             * "?" 只能出现在 日，星期 字段里，表示不知道具体的值
             * "L" 只能出现在 日，星期 字段里，是Last的缩写，一个月的最后一天，一个星期的最后一天（星期六）
             * "W" 表示工作日，距离给定值最近的工作日
             * "#" 表示一个月的第几个星期几，例如：6#3 表示每个月的第三个星期五（1=SUN,...,6=FRI,7=SAT）
             */

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
