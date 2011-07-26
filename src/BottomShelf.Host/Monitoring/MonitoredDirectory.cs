using System;
using System.IO;
using System.Timers;
using Stateless;

namespace BottomShelf.Host.Monitoring
{
    public class MonitoredDirectory
    {
        private readonly FileSystemWatcher fileSystemWatcher;
        private readonly StateMachine<State, FileAction> stateMachine;
        private readonly Timer timer;
        private string path;

        public MonitoredDirectory(FileSystemWatcher fileSystemWatcher, string path, int fileSystemPoll)
        {
            this.fileSystemWatcher = fileSystemWatcher;
            this.path = path;

            timer = new Timer(fileSystemPoll);
            timer.Elapsed += CheckIfFilesAreLocked;

            stateMachine = new StateMachine<State, FileAction>(State.None);

            stateMachine.Configure(State.None)
                .Ignore(FileAction.Unlocked)
                .Permit(FileAction.Created, State.Created)
                .Permit(FileAction.Deleted, State.Deleted)
                .Permit(FileAction.Rename, State.Renamed)
                .Permit(FileAction.Changed, State.Changing);

            stateMachine.Configure(State.Created)
                .OnEntry(OnEnterCreated)
                .OnExit(timer.Stop)
                .Permit(FileAction.Deleted, State.Deleted)
                .Permit(FileAction.Unlocked, State.None);

            stateMachine.Configure(State.Changing)
                .OnEntry(timer.Start)
                .OnExit(OnExitChanging)
                .Permit(FileAction.Unlocked, State.None);

            stateMachine.Configure(State.Renamed)
                .OnEntryFrom(new RenameTrigger(), OnEnterRenamed)
                .Permit(FileAction.Unlocked, State.None);

            stateMachine.Configure(State.Deleted)
                .OnEntry(()=> fileSystemWatcher.RaiseDeleted(path));
        }

        private void OnEnterCreated()
        {
            fileSystemWatcher.RaiseCreated(path);
            timer.Start();
        }

        private void OnEnterRenamed(string newPath)
        {
            fileSystemWatcher.RaiseRenamed(path, newPath);
            path = newPath;
            timer.Start();
        }

        private void OnExitChanging()
        {
            timer.Stop();
            fileSystemWatcher.RaiseChanged(path);
        }

        public void Create()
        {
            stateMachine.Fire(FileAction.Created);
        }

        public void Change()
        {
            stateMachine.Fire(FileAction.Changed);
        }

        public void Delete()
        {
            stateMachine.Fire(FileAction.Deleted);
        }

        public void Rename(string rootServiceDirectory)
        {
            stateMachine.Fire(new RenameTrigger(), rootServiceDirectory);
        }

        private void CheckIfFilesAreLocked(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach(var file in Directory.GetFiles(path))
                    File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None).Dispose();

                stateMachine.Fire(FileAction.Unlocked);
            }
            catch(IOException)
            {
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private enum FileAction
        {
            Changed,
            Unlocked,
            Deleted,
            Rename,
            Created,
        }

        private enum State
        {
            None,
            Created,
            Changing,
            Deleted,
            Renamed,
        }

        private class RenameTrigger : StateMachine<State, FileAction>.TriggerWithParameters<string>
        {
            public RenameTrigger() : base(FileAction.Rename)
            {
            }
        }
    }
}