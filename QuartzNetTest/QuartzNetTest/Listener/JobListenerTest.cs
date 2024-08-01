using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using static QuartzNetTest.Trigger.CronTriggerTest;

namespace QuartzNetTest.Listener
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/trigger-and-job-listeners.html#trigger-and-job-listeners
    /// </summary>
    internal class JobListenerTest
    {
        public static void Start()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.ListenerManager.AddJobListener(new MyJobListener(), KeyMatcher<JobKey>.KeyEquals(new JobKey("myjob4", "group1")));
            scheduler.Start().Wait();
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
    }

    public class MyJobListener : IJobListener
    {
        public string Name { get { return "MyJobListener"; } }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job {context.JobDetail.Key} to be executed!");

        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job {context.JobDetail.Key} was executed!");
        }
    }
}
