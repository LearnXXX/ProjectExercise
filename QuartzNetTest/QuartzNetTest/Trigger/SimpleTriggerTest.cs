using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Impl;
using Quartz;
using static QuartzNetTest.Trigger.CronTriggerTest;

namespace QuartzNetTest.Trigger
{
    internal class SimpleTriggerTest
    {
        public static void Start()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.Start().Wait();


            ///Build a trigger for a specific moment in time, with no repeats:
            var trigger1 = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity("simpleTrigger1", "group1")
                .StartAt(DateTime.Now.AddSeconds(3))
                .ForJob("myjob1", "group1")
                .Build();
            var myjob1 = JobBuilder.Create<SimpleJob1>().WithIdentity("myjob1", "group1").Build();
            var result1 = scheduler.ScheduleJob(myjob1, trigger1).Result;

            //Build a trigger for a specific moment in time, then repeating every ten seconds ten times:

            var trigger2 = TriggerBuilder.Create()
                .WithIdentity("simpleTrigger2", "group1")
                .StartAt(DateTime.Now.AddSeconds(1))
                .WithSimpleSchedule(x=>x.WithIntervalInSeconds(1)
                .WithRepeatCount(10))
                .ForJob("myjob2", "group1")
                .Build();
            var myjob2= JobBuilder.Create<SimpleJob2>().WithIdentity("myjob2", "group1").Build();
            var result2 = scheduler.ScheduleJob(myjob2, trigger2).Result;

            //Build a trigger that will fire once, five minutes in the future:
            var trigger3 = TriggerBuilder.Create()
                .WithIdentity("simpleTrigger3", "group1")
                .StartAt(DateBuilder.FutureDate(5,IntervalUnit.Minute))
                .ForJob("myjob3", "group1")
                .Build();

            var myjob3 = JobBuilder.Create<SimpleJob3>().WithIdentity("myjob3", "group1").Build();
            var result3 = scheduler.ScheduleJob(myjob3, trigger3).Result;

            //Build a trigger that will fire now, then repeat every five minutes, until the hour 22:00:
            var trigger4 = TriggerBuilder.Create()
                .WithIdentity("simpleTrigger4", "group1")
                .WithSimpleSchedule(x=>x.WithIntervalInMinutes(5).RepeatForever())
                .EndAt(DateBuilder.DateOf(22,0,0))
                .ForJob("myjob4", "group1")
                .Build();
            var myjob4 = JobBuilder.Create<SimpleJob4>().WithIdentity("myjob4", "group1").Build();
            var result4 = scheduler.ScheduleJob(myjob4, trigger4).Result;

            //Build a trigger that will fire at the top of the next hour, then repeat every 2 hours, forever:
            var trigger5 = TriggerBuilder.Create()
                .WithIdentity("simpleTrigger5", "group1")
                .StartAt(DateBuilder.EvenHourDate(null))
                .WithSimpleSchedule(x=>x.WithIntervalInHours(2).RepeatForever())
                .ForJob("myjob5", "group1")
                .Build();

            var myjob5= JobBuilder.Create<SimpleJob5>().WithIdentity("myjob5", "group1").Build();
            var result5 = scheduler.ScheduleJob(myjob5, trigger5).Result;

            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        public class SimpleJob1 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is SimpleJob1!");
            }
        }
        public class SimpleJob2 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is SimpleJob2!");
            }
        }
        public class SimpleJob3 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is SimpleJob3!");
            }
        }
        public class SimpleJob4 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is SimpleJob4!");
            }
        }
        public class SimpleJob5 : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}]  This is SimpleJob5!");
            }
        }
    }
}
