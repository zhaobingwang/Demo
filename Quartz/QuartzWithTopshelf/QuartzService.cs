using NLog;
using Quartz;
using Quartz.Impl;
using QuartzWithTopshelf.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace QuartzWithTopshelf
{
    public sealed class QuartzService
    {
        private readonly IScheduler scheduler;
        private static Logger logger;
        public QuartzService()
        {
            logger = LogManager.GetCurrentClassLogger();

            // 1.创建作业调度池(Scheduler)
            scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            //2.创建一个具体的作业即job (具体的job需要单独在一个文件中执行)
            var job = JobBuilder.Create<OutputTimeJob>().Build();
            //3.创建并配置一个触发器即trigger   3s执行一次
            var trigger = TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(3)
.RepeatForever()).Build();
            //4.将job和trigger加入到作业调度池中
            scheduler.ScheduleJob(job, trigger);
        }

        public void Start()
        {
            scheduler.Start();
            logger.Info($"Job {nameof(OutputTimeJob)} start...");
        }

        public void Stop()
        {
            //true：表示该Sheduler关闭之前需要等现在所有正在运行的工作完成才能关闭
            //false：表示直接关闭
            scheduler.Shutdown(false);
            logger.Info($"Job {nameof(OutputTimeJob)} shutdown...");
        }

        public void Continue()
        {
            scheduler.ResumeAll();
            logger.Info($"Job {nameof(OutputTimeJob)} comtinue...");
        }

        public void Pause()
        {
            scheduler.PauseAll();
            logger.Info($"Job {nameof(OutputTimeJob)} pause...");
        }
    }
}
