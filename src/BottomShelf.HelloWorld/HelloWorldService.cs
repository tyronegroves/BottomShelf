namespace BottomShelf.HelloWorld
{
    public class HelloWorldService : HostedServiceBase
    {
        public CronTriggerExample CronTriggerExample { get; set; }

        public HelloWorldService()
        {
            CronTriggerExample = new CronTriggerExample();
        }

        public override void Start()
        {
            CronTriggerExample.Run();
        }

        public override void Stop()
        {
            CronTriggerExample.End();
        }
    }
}