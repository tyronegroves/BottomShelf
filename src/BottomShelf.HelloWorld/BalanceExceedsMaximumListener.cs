using Quartz;

namespace BottomShelf.HelloWorld
{
    public class BalanceExceedsMaximumListener : IJobListener
    {
        public void JobToBeExecuted(IJobExecutionContext context)
        {
            // This state will actually never be reached
            if ((int) context.JobDetail.JobDataMap.Get(CashBalanceInquiryJob.CashOnHand) <= 0)
            {
                context.JobDetail.JobDataMap.Put(CashBalanceInquiryJob.CashOnHand, 1);
            }
        }

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            if ((int)context.JobDetail.JobDataMap.Get(CashBalanceInquiryJob.CashOnHand) > 100)
            {
                context.JobDetail.JobDataMap.Put(CashBalanceInquiryJob.CashOnHand, 1);
            }
        }

        public string Name
        {
            get { return "Balance exceeds maximum"; }
        }
    }
}