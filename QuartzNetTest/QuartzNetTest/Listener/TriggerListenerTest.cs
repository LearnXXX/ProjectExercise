using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl;
using static QuartzNetTest.Trigger.CronTriggerTest;

namespace QuartzNetTest.Listener
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/trigger-and-job-listeners.html#trigger-and-job-listeners
    /// </summary>
    internal class TriggerListenerTest
    {
        public static void Start()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.ListenerManager.AddTriggerListener(new MyTriggerListener(), KeyMatcher<TriggerKey>.KeyEquals(new TriggerKey("cronTrigger4", "group1")));
            scheduler.Start().Wait();
            ITrigger trigger4 = TriggerBuilder.Create()
                .WithIdentity("cronTrigger4", "group1")
                .WithCronSchedule("* * * ? * * *")
                .ForJob("myjob4", "group1")
                .Build();

            var myjob4 = JobBuilder.Create<Job4>().WithIdentity("myjob4", "group1").Build();
            var result4 = scheduler.ScheduleJob(myjob4, trigger4).Result;

            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            scheduler.Shutdown().Wait();
        }
    }
    public class MyTriggerListener : ITriggerListener
    {
        public string Name { get { return "MyTriggerListener"; } }

        public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"[{DateTime.Now}] the trigger {trigger.Key} complete!");
        }

        public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"[{DateTime.Now}] the trigger {trigger.Key} fired, the job is {context.JobDetail.Key}!");
        }

        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"[{DateTime.Now}] the trigger {trigger.Key} Misfired!");
        }

        public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => { return false; });
        }
    }
}
