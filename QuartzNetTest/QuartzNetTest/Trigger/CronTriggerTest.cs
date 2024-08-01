using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using QuartzNetTest.Job;

namespace QuartzNetTest.Trigger
{
    /// <summary>
    /// document：https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html
    /// cron expression generator: https://www.freeformatter.com/cron-expression-generator-quartz.html
    ///                            http://www.cronmaker.com/
    /// 
    /// </summary>
    internal class CronTriggerTest
    {
        public static void Start()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.Start().Wait();


            ///Build a trigger that will fire every other minute, between 8am and 5pm, every day:
            var trigger1 = TriggerBuilder.Create().WithIdentity("cronTrigger1", "group1")
                .WithCronSchedule("0 0/2 8-17 * * ?")
                .ForJob("myjob1", "group1")
                .Build();
            var myjob1 = JobBuilder.Create<Job1>().WithIdentity("myjob1", "group1").Build();
            var result1 = scheduler.ScheduleJob(myjob1, trigger1).Result;


            //Build a trigger that will fire daily at 10:42 am:
            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("tragger1", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(15, 26))
                .ForJob("myjob2", "group1")
                .Build();
            var myjob2 = JobBuilder.Create<Job2>().WithIdentity("myjob2", "group1").Build();
            var result2 = scheduler.ScheduleJob(myjob2, trigger2).Result;


            //Build a trigger that will fire daily at 10:42 am with cron expression
            ITrigger trigger3 = TriggerBuilder.Create()
                .WithIdentity("cronTrigger3", "group1")
                .WithCronSchedule("0 26 15 * * ?")
                .ForJob("myjob3", "group1")
                .Build();
            var myjob3 = JobBuilder.Create<Job3>().WithIdentity("myjob3", "group1").Build();
            var result3 = scheduler.ScheduleJob(myjob3, trigger3).Result;


            //Build a trigger that will fire every second cron expression
            ITrigger trigger4 = TriggerBuilder.Create()
                .WithIdentity("cronTrigger4", "group1")
                .WithCronSchedule("* * * ? * * *")
                .ForJob("myjob4", "group1")
                .Build();
            var myjob4 = JobBuilder.Create<Job4>().WithIdentity("myjob4", "group1").Build();
            var result4 = scheduler.ScheduleJob(myjob4, trigger4).Result;

            Task.Delay(TimeSpan.FromSeconds(100)).Wait();
            scheduler.Shutdown().Wait();
        }
        internal class Job1 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is job1!");
            }
        }
        internal class Job2 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is job2!");
            }
        }
        internal class Job3 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is job3!");
            }
        }
        internal class Job4 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is job4!");
            }
        }
    }
}
