using System;
using System.IO;
using System.Timers;
using Stateless;

namespace BottomShelf.Host.Monitoring
{
    public class FileSystemWatcherEx
    {
        private FileSystemWatcher internalWatcher;

        public void Start(string path)
        {
            internalWatcher = new FileSystemWatcher(path);
            internalWatcher.Created += OnInternalWatcherCreated;
        }

        private void OnInternalWatcherCreated(object sender, FileSystemEventArgs e)
        {
        }
    }

    public class MonitoredFile
    {
        private readonly string filePath;
        private StateMachine<State, FileAction> stateMachine;
        private Timer timer;

        public MonitoredFile(string filePath)
        {
            this.filePath = filePath;
            
            timer = new Timer(1000);
            timer.Elapsed += CheckIfFileIsLocked;

            stateMachine = new StateMachine<State, FileAction>(State.Created);

            stateMachine.Configure(State.Created)
                .Permit(FileAction.Changed, State.Changing);

            stateMachine.Configure(State.Changing)
                .OnEntry(timer.Start)
                .OnExit(timer.Stop)
                .Permit(FileAction.Unlocked, State.Ready)
                .PermitReentry(FileAction.Changed);
        }

        private void CheckIfFileIsLocked(object sender, ElapsedEventArgs e)
        {
            try
            {
                File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None).Dispose();
                stateMachine.Fire(FileAction.Unlocked);
            }
            catch(IOException)
            {
            }
        }

        private enum FileAction
        {
            Changed,
            Unlocked
        }

        private enum State
        {
            Created,
            Changing,
            Ready
        }
    }
}