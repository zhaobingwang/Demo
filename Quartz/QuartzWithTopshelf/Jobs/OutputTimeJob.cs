using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Task Execute(IJobExecutionContext context)
        {
            //var config = new NLog.Config.LoggingConfiguration();

            //var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"{DateTime.Now.ToString("yyyyMMdd")}.log" };
            //var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            //config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            //config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            //LogManager.Configuration = config;

            Task t = Task.Factory.StartNew(() =>
            {
                logger.Info($"CurrentTime:{DateTime.Now}");
                logger.Warn("hahahaha");
            });

            return t;
        }
    }
}
