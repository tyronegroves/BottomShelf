using System;
using TechTalk.SpecFlow;

namespace BottomShelf.Host.Specs.Steps
{
    [Binding]
    public class LogManagerSteps
    {
        [Given(@"the log factory method will return a log of type '(.*)' for '(.*)'")]
        public void GivenISetTheLogFactoryMethodToReturnALogOfTypeForAType(string logTypeName, string requestedTypeName)
        {
            var logType = Type.GetType(logTypeName);
            var requestedType = Type.GetType(requestedTypeName);
            var logFactoryMethod = LogManager.GetLogFactoryMethod();

            LogManager.SetLogFactoryMethod(t =>
                                               {
                                                   if(t == requestedType)
                                                       return (ILog)Activator.CreateInstance(logType);
                                                   return logFactoryMethod(t);
                                               });
        }

        [When(@"I ask for the log using '(.*)' as the type")]
        public void WhenIAskForTheILogUsingNullAsTheType(string typeName)
        {
            var type = Type.GetType(typeName);
            var log = LogManager.GetLog(type);

            ScenarioContext.Current.Set(log);
        }
    }
}