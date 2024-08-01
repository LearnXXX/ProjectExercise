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
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/scheduler-listeners.html
    /// </summary>
    internal class SchedulerListenerTest
    {
        public static void Start()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.ListenerManager.AddSchedulerListener(new MySchedulerListener());
            scheduler.Start().Wait();
            ITrigger trigger4 = TriggerBuilder.Create()
                .WithIdentity("cronTrigger4", "group1")
                .WithCronSchedule("* * * ? * * *")
                .ForJob("myjob4", "group1")
                .Build();

            var myjob4 = JobBuilder.Create<Job4>().WithIdentity("myjob4", "group1").Build();
            var result4 = scheduler.ScheduleJob(myjob4, trigger4).Result;

            scheduler.PauseJob(new JobKey("myjob4", "group1"));
            scheduler.ResumeJob(new JobKey("myjob4", "group1"));

            scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals("group1"));
            scheduler.ResumeJobs(GroupMatcher<JobKey>.GroupEquals("group1"));

            scheduler.PauseTrigger(new TriggerKey("cronTrigger4", "group1"));
            scheduler.ResumeTrigger(new TriggerKey("cronTrigger4", "group1"));


            scheduler.PauseTriggers(GroupMatcher<TriggerKey>.GroupEquals("group1"));
            scheduler.ResumeTriggers(GroupMatcher<TriggerKey>.GroupEquals("group1"));


            Task.Delay(TimeSpan.FromSeconds(10)).Wait();

            scheduler.Shutdown().Wait();
        }

        public class MySchedulerListener : ISchedulerListener
        {
            public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job {jobDetail.Key} was added to the scheduler!");
            }

            public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job {jobKey} was deleted from the scheduler!");
            }

            public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job {jobKey} was interrupted!");
            }

            public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job {jobKey} was paused!");
            }

            public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job {jobKey} was resumed!");
            }

            public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job {trigger.JobKey} scheduled by trigger {trigger.Key}!");
            }

            public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job in the group {jobGroup} was paused!");
            }

            public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the job in the group {jobGroup} was resumed!");
            }

            public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] unscheduled by trigger {triggerKey.ToString()}!");
            }

            public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] scheduled error! {msg}");
            }

            public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
            {
            }

            public async Task SchedulerShutdown(CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] Scheduler was shut down!");
            }

            public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] Scheduler is shutting down!");
            }

            public async Task SchedulerStarted(CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] Scheduler was started!");
            }

            public async Task SchedulerStarting(CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] Scheduler is starting!");
            }

            public async Task SchedulingDataCleared(CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] Scheduler data was cleared!");
            }

            public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the trigger {trigger.Key} was finalized!");
            }

            public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the trigger {triggerKey.ToString()} was paused!");
            }

            public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the trigger {triggerKey.ToString()} was resumed!");
            }

            public async Task TriggersPaused(string? triggerGroup, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the triggers in the {triggerGroup} was paused!");
            }

            public async Task TriggersResumed(string? triggerGroup, CancellationToken cancellationToken = default)
            {
                await Console.Out.WriteLineAsync($"[{DateTime.Now}] the triggers in the {triggerGroup} was resumed!");
            }
        }
    }
}
