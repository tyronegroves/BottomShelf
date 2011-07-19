using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BottomShelf.Host.Monitoring;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BottomShelf.Host.Specs.Steps
{
    [Binding]
    public class DirectoryMonitorSteps
    {
        public List<FileChangedEventArgs> FileChangedEvents
        {
            get { return ScenarioContext.Current.Get<List<FileChangedEventArgs>>("FileChangedEvents"); }
            set { ScenarioContext.Current.Set(value, "FileChangedEvents"); }
        }

        [BeforeScenario]
        public void SetupDirectoryMonitor()
        {
            FileChangedEvents = new List<FileChangedEventArgs>();

            var directoryMonitor = new DirectoryMonitor();
            directoryMonitor.DirectoryCreated += (sender, e) => ScenarioContext.Current.Set(e);
            directoryMonitor.DirectoryRenamed += (sender, e) => ScenarioContext.Current.Set(e);
            directoryMonitor.FileCreated += (sender, e) => ScenarioContext.Current.Set(e);
            directoryMonitor.FileRenamed += (sender, e) => ScenarioContext.Current.Set(e);
            directoryMonitor.FileChanged += (sender, e) => FileChangedEvents.Add(e);
            directoryMonitor.ItemDeleted += (sender, e) => ScenarioContext.Current.Set(e);

            ScenarioContext.Current.Set(directoryMonitor);
        }

        [AfterScenario]
        public void StopDirectoryMonitor()
        {
            FileChangedEvents.Clear();
            var directoryMonitor = ScenarioContext.Current.Get<DirectoryMonitor>();
            directoryMonitor.Stop();
        }

        [Given(@"I am monitoring the directory '(.*)'")]
        public void GivenIAmMonitoringTheDirectory(string directory)
        {
            directory = FileSystemSteps.MakeTestDirectory(directory);

            var monitor = ScenarioContext.Current.Get<DirectoryMonitor>();
            monitor.Start(directory);

            ScenarioContext.Current.Set(monitor);
        }

        [Then(@"the directory monitor should notify that the directory '(.*)' was created")]
        public void ThenTheDirectoryMonitorIsNotifiedThatTheDirectoryWasCreated(string directory)
        {
            Thread.Sleep(10);

            var directoryCreatedEventArgs = ScenarioContext.Current.Get<DirectoryCreatedEventArgs>();

            Assert.AreEqual(FileSystemSteps.MakeTestDirectory(directory), directoryCreatedEventArgs.Path);
        }

        [Then(@"the directory monitor should notify that the directory '(.*)' was renamed to '(.*)'")]
        public void ThenTheDirectoryMonitorShouldNotifyThatTheDirectoryWasRenamed(string originalDirectory, string newDirectory)
        {
            Thread.Sleep(10);

            var directoryRenamedEventArgs = ScenarioContext.Current.Get<DirectoryRenamedEventArgs>();

            Assert.AreEqual(FileSystemSteps.MakeTestDirectory(originalDirectory), directoryRenamedEventArgs.OriginalPath);
            Assert.AreEqual(FileSystemSteps.MakeTestDirectory(newDirectory), directoryRenamedEventArgs.Path);
        }

        [Then(@"the directory monitor should notify that the file '(.*)' was created")]
        public void ThenTheDirectoryMonitorShouldNotifyThatAFileWasAddedToTheDirectory(string fileName)
        {
            Thread.Sleep(10);

            var fileAddedEventArgs = ScenarioContext.Current.Get<FileCreatedEventArgs>();

            var filePath = Path.Combine(FileSystemSteps.TestBaseDirectory, fileName);
            var directory = Path.GetDirectoryName(filePath);

            Assert.AreEqual(directory, fileAddedEventArgs.DirectoryPath);
            Assert.AreEqual(filePath, fileAddedEventArgs.FilePath);
        }

        [Then(@"the directory monitor should notify that the file '(.*)' was deleted")]
        public void ThenTheDirectoryMonitorShouldNotifyThatTheFileWasDeleted(string fileName)
        {
            Thread.Sleep(10);

            var filePath = Path.Combine(FileSystemSteps.TestBaseDirectory, fileName);
            var itemDeletedEventArgs = ScenarioContext.Current.Get<ItemDeletedEventArgs>();

            Assert.AreEqual(filePath, itemDeletedEventArgs.Path);
        }

        [Then(@"the directory monitor should notify that the directory '(.*)' was deleted")]
        public void ThenTheDirectoryMonitorShouldNotifyThatTheDirectoryWasDeleted(string directory)
        {
            Thread.Sleep(10);

            directory = FileSystemSteps.MakeTestDirectory(directory);

            var itemDeletedEventArgs = ScenarioContext.Current.Get<ItemDeletedEventArgs>();

            Assert.AreEqual(directory, itemDeletedEventArgs.Path);
        }

        [Then(@"the directory monitor should notify that the file '(.*)' was renamed to '(.*)'")]
        public void ThenTheDirectoryMonitorShouldNotifyThatTheFileWasRenamed(string originalFilePath, string newFilePath)
        {
            Thread.Sleep(10);

            originalFilePath = FileSystemSteps.MakeTestDirectory(originalFilePath);
            newFilePath = FileSystemSteps.MakeTestDirectory(newFilePath);

            var fileRenamedEventArgs = ScenarioContext.Current.Get<FileRenamedEventArgs>();

            Assert.AreEqual(originalFilePath, fileRenamedEventArgs.OriginalFilePath);
            Assert.AreEqual(newFilePath, fileRenamedEventArgs.FilePath);
        }

        [Then(@"the directory monitor should notify that the file '(.*)' was changed")]
        public void ThenTheDirectoryMonitorShouldNotifyThatTheFileWasChanged(string filePath)
        {
            filePath = FileSystemSteps.MakeTestDirectory(filePath);

            Assert.AreEqual(1, FileChangedEvents.Count);
            Assert.AreEqual(filePath, FileChangedEvents[0].Path);
        }
    }
}