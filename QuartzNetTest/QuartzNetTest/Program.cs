// See https://aka.ms/new-console-template for more information

/*
 * Quartz.NET is a full-featured, open source job scheduling system that can be used from smallest apps to large scale enterprise systems.
 * Offical website: https://www.quartz-scheduler.net/
 */

using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using QuartzNetTest;
using QuartzNetTest.Job;
using QuartzNetTest.Listener;
using QuartzNetTest.Trigger;

LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
SchedulerListenerTest.Start();
TriggerListenerTest.Start();
JobListenerTest.Start();
SimpleTriggerTest.Start();
CronTriggerTest.Start();

