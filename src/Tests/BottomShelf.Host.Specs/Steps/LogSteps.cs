using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BottomShelf.Host.Specs.Steps
{
    [Binding]
    public class LogSteps
    {
        [Then(@"the log should be of type '(.*)'")]
        public void ThenTheILogShouldBeOfType(string typeName)
        {
            var exceptedType = Type.GetType(typeName);
            var log = ScenarioContext.Current.Get<ILog>();

            Assert.IsInstanceOf(exceptedType, log);
        }
    }
}