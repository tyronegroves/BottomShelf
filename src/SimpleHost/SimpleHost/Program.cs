using System;
using System.IO;
using System.Threading;
using SimpleHost.Monitoring;
using SimpleHost.Monitoring.Events;

namespace SimpleHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string path = @"C:\_Application\SimpleHost\src\SimpleHost\SimpleHost\bin\Debug\Services";

            var serviceDirectories = Directory.GetDirectories(path);
            var hostedServiceManager = new HostedServiceManager(new HostedServiceScanner(), new HostedServiceLoader());
            var watcher = new QueueFileSystemWatcher(path);
            watcher.Start();

            foreach(var serviceDirectory in serviceDirectories)
            {
                hostedServiceManager.LoadFromDirectory(serviceDirectory);
            }

            Console.WriteLine("Running");

            var cancelEvent = new ManualResetEvent(false);
            var fileEvent = new AutoResetEvent(false);

            watcher.FileSystemEventQueued += (sender, e) => fileEvent.Set();

            while(WaitHandle.WaitAny(new WaitHandle[] {cancelEvent, fileEvent}) != 0)
            {
                QueuedFileSystemEvent fileSystemEvent;
                while((fileSystemEvent = watcher.Dequeue()) != null)
                {
                    if(fileSystemEvent is DirectoryDeletedEvent)
                    {
                        hostedServiceManager.UnloadAllInDirectory(((DirectoryDeletedEvent)fileSystemEvent).FullDirectoryPath);
                    }

                    if(fileSystemEvent is DirectoryCreatedEvent)
                    {
                        hostedServiceManager.LoadFromDirectory(((DirectoryCreatedEvent)fileSystemEvent).FullDirectoryPath);
                    }

                    Console.WriteLine("Event {0}", fileSystemEvent);
                }
            }

            Console.WriteLine("Terminating");

            hostedServiceManager.UnloadAllHostedServices();

            Console.ReadLine();
        }
    }
}