using System.IO;
using TechTalk.SpecFlow;

namespace BottomShelf.Host.Specs.Steps
{
    [Binding]
    public class FileSystemSteps
    {
        public static string TestBaseDirectory
        {
            get { return ".\\Test"; }
        }

        public static string MakeTestDirectory(string directory)
        {
            return Path.Combine(TestBaseDirectory, directory);
        }

        [BeforeTestRun]
        public static void SetupScenarioTestDirectory()
        {
            if(Directory.Exists(TestBaseDirectory))
                Directory.Delete(TestBaseDirectory, true);

            if(!Directory.Exists(TestBaseDirectory))
                Directory.CreateDirectory(TestBaseDirectory);
        }

        [Given(@"the directory '(.*)' exists")]
        public void GivenTheDirectoryExists(string directory)
        {
            directory = MakeTestDirectory(directory);

            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        [When(@"the file '(.*)' is created")]
        [Given(@"the file '(.*)' exists")]
        public void GivenTheFileExists(string fileName)
        {
            var filePath = Path.Combine(TestBaseDirectory, fileName);
            var directory = Path.GetDirectoryName(filePath);

            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.Create(filePath).Dispose();
        }

        [When(@"the directory '(.*)' is created")]
        public void WhenTheDirectoryIsCreated(string directory)
        {
            directory = Path.Combine(TestBaseDirectory, directory);

            Directory.CreateDirectory(directory);
        }

        [When(@"the directory '(.*)' is renamed to '(.*)'")]
        public void WhenADirectoryIsRenamed(string originalDirectory, string newDirectory)
        {
            originalDirectory = MakeTestDirectory(originalDirectory);
            newDirectory = MakeTestDirectory(newDirectory);

            Directory.Move(originalDirectory, newDirectory);
        }

        [When(@"the file '(.*)' is deleted")]
        public void WhenTheFileIsDeleted(string fileName)
        {
            var filePath = Path.Combine(TestBaseDirectory, fileName);

            File.Delete(filePath);
        }

        [When(@"the directory '(.*)' is deleted")]
        public void WhenTheDirectoryIsDeleted(string directory)
        {
            directory = MakeTestDirectory(directory);
            Directory.Delete(directory);
        }
    }
}