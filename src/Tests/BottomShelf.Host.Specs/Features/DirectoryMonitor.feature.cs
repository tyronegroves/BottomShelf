// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.6.0.0
//      SpecFlow Generator Version:1.6.0.0
//      Runtime Version:4.0.30319.225
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace BottomShelf.Host.Specs.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.6.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("DirectoryMonitor")]
    public partial class DirectoryMonitorFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "DirectoryMonitor.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "DirectoryMonitor", "In order to monitor directories\r\nAs the system\r\nI want to be notified when files " +
                    "and directories have been created, changed, or removed within a directory being " +
                    "monitored", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A directory is created in the directory being monitored")]
        public virtual void ADirectoryIsCreatedInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A directory is created in the directory being monitored", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("the directory \'MyDirectory1\' exists");
#line 8
 testRunner.And("I am monitoring the directory \'MyDirectory1\'");
#line 9
 testRunner.When("the directory \'MyDirectory1\\MyCreatedDirectory\' is created");
#line 10
 testRunner.Then("the directory monitor should notify that the directory \'MyDirectory1\\MyCreatedDir" +
                    "ectory\' was created");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A directory two levels deep is created in the directory being monitored")]
        public virtual void ADirectoryTwoLevelsDeepIsCreatedInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A directory two levels deep is created in the directory being monitored", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
 testRunner.Given("the directory \'MyDirectory6\\MyParentDirectory\' exists");
#line 14
 testRunner.And("I am monitoring the directory \'MyDirectory6\'");
#line 15
 testRunner.When("the directory \'MyDirectory6\\MyParentDirectory\\MyCreatedSubDirectory\' is created");
#line 16
 testRunner.Then("the directory monitor should notify that the directory \'MyDirectory6\\MyParentDire" +
                    "ctory\\MyCreatedSubDirectory\' was created");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A directory in the directory being monitored is renamed")]
        public virtual void ADirectoryInTheDirectoryBeingMonitoredIsRenamed()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A directory in the directory being monitored is renamed", ((string[])(null)));
#line 18
this.ScenarioSetup(scenarioInfo);
#line 19
 testRunner.Given("the directory \'MyDirectory2\\MyRenamedDirectory\' exists");
#line 20
 testRunner.And("I am monitoring the directory \'MyDirectory2\'");
#line 21
 testRunner.When("the directory \'MyDirectory2\\MyRenamedDirectory\' is renamed to \'MyDirectory2\\MyRen" +
                    "amedDirectory2\'");
#line 22
 testRunner.Then("the directory monitor should notify that the directory \'MyDirectory2\\MyRenamedDir" +
                    "ectory\' was renamed to \'MyDirectory2\\MyRenamedDirectory2\'");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A directory two levels deep in the directory being monitored is renamed")]
        public virtual void ADirectoryTwoLevelsDeepInTheDirectoryBeingMonitoredIsRenamed()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A directory two levels deep in the directory being monitored is renamed", ((string[])(null)));
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("the directory \'MyDirectory5\\MyParentDirectory\\SubDirectory\' exists");
#line 26
 testRunner.And("I am monitoring the directory \'MyDirectory5\'");
#line 27
 testRunner.When("the directory \'MyDirectory5\\MyParentDirectory\\SubDirectory\' is renamed to \'MyDire" +
                    "ctory5\\MyParentDirectory\\SubRenamedDirectory\'");
#line 28
 testRunner.Then("the directory monitor should notify that the directory \'MyDirectory5\\MyParentDire" +
                    "ctory\\SubDirectory\' was renamed to \'MyDirectory5\\MyParentDirectory\\SubRenamedDir" +
                    "ectory\'");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A directory is deleted in the directory being monitored")]
        public virtual void ADirectoryIsDeletedInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A directory is deleted in the directory being monitored", ((string[])(null)));
#line 30
this.ScenarioSetup(scenarioInfo);
#line 31
 testRunner.Given("the directory \'MyDirectory6\\Deleted\' exists");
#line 32
 testRunner.And("I am monitoring the directory \'MyDirectory6\'");
#line 33
 testRunner.When("the directory \'MyDirectory6\\Deleted\' is deleted");
#line 34
 testRunner.Then("the directory monitor should notify that the directory \'MyDirectory6\\Deleted\' was" +
                    " deleted");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A directory two levels deep is deleted in the directory being monitored")]
        public virtual void ADirectoryTwoLevelsDeepIsDeletedInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A directory two levels deep is deleted in the directory being monitored", ((string[])(null)));
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("the directory \'MyDirectory6\\Parent\\Deleted\' exists");
#line 38
 testRunner.And("I am monitoring the directory \'MyDirectory6\'");
#line 39
 testRunner.When("the directory \'MyDirectory6\\Parent\\Deleted\' is deleted");
#line 40
 testRunner.Then("the directory monitor should notify that the directory \'MyDirectory6\\Parent\\Delet" +
                    "ed\' was deleted");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A file is created in the directory being monitored")]
        public virtual void AFileIsCreatedInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A file is created in the directory being monitored", ((string[])(null)));
#line 42
this.ScenarioSetup(scenarioInfo);
#line 43
 testRunner.Given("the directory \'MyDirectory3\' exists");
#line 44
 testRunner.And("I am monitoring the directory \'MyDirectory3\'");
#line 45
 testRunner.When("the file \'MyDirectory3\\File1.txt\' is created");
#line 46
 testRunner.Then("the directory monitor should notify that the file \'MyDirectory3\\File1.txt\' was cr" +
                    "eated");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A file is created two levels deep in the directory being monitored")]
        public virtual void AFileIsCreatedTwoLevelsDeepInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A file is created two levels deep in the directory being monitored", ((string[])(null)));
#line 48
this.ScenarioSetup(scenarioInfo);
#line 49
 testRunner.Given("the directory \'MyDirectory3\' exists");
#line 50
 testRunner.And("I am monitoring the directory \'MyDirectory3\'");
#line 51
 testRunner.When("the file \'MyDirectory3\\ParentDirectory\\File3.txt\' is created");
#line 52
 testRunner.Then("the directory monitor should notify that the file \'MyDirectory3\\ParentDirectory\\F" +
                    "ile3.txt\' was created");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A file is deleted from the directory being monitored")]
        public virtual void AFileIsDeletedFromTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A file is deleted from the directory being monitored", ((string[])(null)));
#line 54
this.ScenarioSetup(scenarioInfo);
#line 55
 testRunner.Given("the file \'MyDirectory4\\File2.txt\' exists");
#line 56
 testRunner.And("I am monitoring the directory \'MyDirectory4\'");
#line 57
 testRunner.When("the file \'MyDirectory4\\File2.txt\' is deleted");
#line 58
 testRunner.Then("the directory monitor should notify that the file \'MyDirectory4\\File2.txt\' was de" +
                    "leted");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A file is deleted two levels deep from the directory being monitored")]
        public virtual void AFileIsDeletedTwoLevelsDeepFromTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A file is deleted two levels deep from the directory being monitored", ((string[])(null)));
#line 60
this.ScenarioSetup(scenarioInfo);
#line 61
 testRunner.Given("the file \'MyDirectory4\\ParentDirectory\\File2.txt\' exists");
#line 62
 testRunner.And("I am monitoring the directory \'MyDirectory4\'");
#line 63
 testRunner.When("the file \'MyDirectory4\\ParentDirectory\\File2.txt\' is deleted");
#line 64
 testRunner.Then("the directory monitor should notify that the file \'MyDirectory4\\ParentDirectory\\F" +
                    "ile2.txt\' was deleted");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A file is renamed in the directory being monitored")]
        public virtual void AFileIsRenamedInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A file is renamed in the directory being monitored", ((string[])(null)));
#line 66
this.ScenarioSetup(scenarioInfo);
#line 67
 testRunner.Given("the file \'MyDirectory7\\File2.txt\' exists");
#line 68
 testRunner.And("I am monitoring the directory \'MyDirectory7\'");
#line 69
 testRunner.When("the file \'MyDirectory7\\File2.txt\' is renamed to \'MyDirectory7\\File3.txt\'");
#line 70
 testRunner.Then("the directory monitor should notify that the file \'MyDirectory7\\File2.txt\' was re" +
                    "named to \'MyDirectory7\\File3.txt\'");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A file is renamed two levels deep in the directory being monitored")]
        public virtual void AFileIsRenamedTwoLevelsDeepInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A file is renamed two levels deep in the directory being monitored", ((string[])(null)));
#line 72
this.ScenarioSetup(scenarioInfo);
#line 73
 testRunner.Given("the file \'MyDirectory7\\Parent2\\File2.txt\' exists");
#line 74
 testRunner.And("I am monitoring the directory \'MyDirectory7\'");
#line 75
 testRunner.When("the file \'MyDirectory7\\Parent2\\File2.txt\' is renamed to \'MyDirectory7\\Parent2\\Fil" +
                    "e3.txt\'");
#line 76
 testRunner.Then("the directory monitor should notify that the file \'MyDirectory7\\Parent2\\File2.txt" +
                    "\' was renamed to \'MyDirectory7\\Parent2\\File3.txt\'");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A file is changed in the directory being monitored")]
        public virtual void AFileIsChangedInTheDirectoryBeingMonitored()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A file is changed in the directory being monitored", ((string[])(null)));
#line 78
this.ScenarioSetup(scenarioInfo);
#line 79
 testRunner.Given("the file \'MyDirectory8\\File7.txt\' exists");
#line 80
 testRunner.And("I am monitoring the directory \'MyDirectory8\'");
#line 81
 testRunner.When("the file \'MyDirectory8\\File7.txt\' size is increased to \'16\' bytes at a rate of \'1" +
                    "6\' bytes every \'1\' milliseconds");
#line 82
 testRunner.Then("the directory monitor should notify that the file \'MyDirectory8\\File7.txt\' was ch" +
                    "anged");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
