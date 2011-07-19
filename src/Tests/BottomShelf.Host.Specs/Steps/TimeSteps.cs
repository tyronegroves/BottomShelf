using System.Threading;
using TechTalk.SpecFlow;

namespace BottomShelf.Host.Specs.Steps
{
    [Binding]
    public class TimeSteps
    {
        [When(@"the is a pause for '(.*)' milliseconds")]
        public void WhenTheIsAPauseForMilliseconds(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}