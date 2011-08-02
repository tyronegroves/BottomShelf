using System;
using System.Threading;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace BottomShelf.HelloWorld
{
    public class CronTriggerExample : ICronTriggerExample
    {
        private IScheduler Schedule { get; set; }

        public string Name
        {
            get { return "Cron job example"; }
        }

        public virtual void Run()
        {
            Schedule = new StdSchedulerFactory().GetScheduler();

            var job = JobBuilder.Create<CashBalanceInquiryJob>().WithIdentity("CashBalanceJob", "FinancialReportsGroup").Build();

            job.JobDataMap.Put(CashBalanceInquiryJob.CashOnHand, 1);

            // Fires every 5 seconds, Saturday through Wednesday
            var trigger = TriggerBuilder.Create().WithIdentity("CalculateCashBalanceTrigger", "FinancialReportsGroup")
                              .WithCronSchedule("0/5 * * ? * SAT-WED")
                              .Build() as ICronTrigger;

            Schedule.ScheduleJob(job, trigger);

            // Not necessary but pre and post-event listeners, usually used to fire off other jobs
            IJobListener listener = new BalanceExceedsMaximumListener();
            var matcher = KeyMatcher<JobKey>.KeyEquals(job.Key);

            Schedule.ListenerManager.AddJobListener(listener, matcher);

            Schedule.Start();
        }

        public void End()
        {
            Schedule.Shutdown();
            
            // To show the schedule really shutdown, wait longer than the job trigger
            Console.WriteLine("Stopping in 10 seconds");
            Thread.Sleep(1000 * 10);
        }
    }
}