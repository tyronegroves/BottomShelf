using System;
using Quartz;

namespace BottomShelf.HelloWorld
{
    [PersistJobDataAfterExecution]
    public class CashBalanceInquiryJob : IJob
    {
        public const string CashOnHand = "CashOnHand";

        public virtual void Execute(IJobExecutionContext context)
        {
            var jobKey = context.JobDetail.Key;
            var data = context.JobDetail.JobDataMap;

            // Perform fake business logic
            var cashOnHand = data.GetInt(CashOnHand);
            data.Put(CashOnHand, cashOnHand + 50);

            Console.WriteLine("{0}", jobKey);
            Console.WriteLine("Cash on hand {0:c}", data.GetInt(CashOnHand));
        }
    }
}