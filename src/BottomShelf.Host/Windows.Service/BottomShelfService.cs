using System;
using System.ServiceProcess;
using CommandLine;

namespace BottomShelf.Host.Windows.Service
{
    internal partial class BottomShelfService : ServiceBase
    {
        private readonly BottomShelfHost bottomShelfHost;

        public BottomShelfService(BottomShelfHost bottomShelfHost)
        {
            this.bottomShelfHost = bottomShelfHost;
            InitializeComponent();
        }

        protected override void OnStart(string[] arguments)
        {
            var parser = new CommandLineParser(new CommandLineParserSettings());
            var commandLineParameters = new CommandLineParameters();

            parser.ParseArguments(arguments, commandLineParameters);

            bottomShelfHost.Start(commandLineParameters);
        }

        protected override void OnStop()
        {
            bottomShelfHost.Stop();
        }
    }
}