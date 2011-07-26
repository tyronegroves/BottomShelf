using System;
using System.Reflection;
using CommandLine;
using CommandLine.Text;

namespace BottomShelf.Host
{
    public class CommandLineParameters : CommandLineOptionsBase
    {
        [Option("i", "install", HelpText = "Installs the host as a windows service.")] 
        public bool InstallAsWindowService;

        [Option("u", "uninstall", HelpText = "Uninstalls the host as a windows service.")] 
        public bool UninstallWindowsService;

        [Option(null, "delayedAutoStart", HelpText = "Set the windows service to delay auto start.")] 
        public bool DelayedAutoStart;

        [Option(null, "startType", HelpText = "Set the start type of the windows service. Options: Automatic, Disable, Manual. The default is 'Automatic'.")] 
        public string StartType = "Automatic";

        [Option(null, "serviceAccount", HelpText = "Set the user account the windows service will run as. Options: LocalService, NetworkService, LocalSystem, User.  The default is 'LocalSystem'")]
        public string ServiceAccount = "LocalSystem";

        [Option(null, "fileSystemPoll", HelpText = "Interval time for service to check if the file is unlocked in milliseconds. Default is 1000 ms.")]
        public int FileSystemPoll = 1000;

        [Option("s","serviceDirectory", HelpText = @"The directory to monitor for services. Default is .\Services")]
        public string ServicesDirectory = @".\Services";

        [HelpOption(null, "help")]
        public string GetUsage()
        {
            var help = new HelpText(new HeadingInfo("BottomShelf Host", Assembly.GetExecutingAssembly().GetName().Version.ToString(2)));
            help.Copyright = new CopyrightInfo("Peasant Coder Foundation", DateTime.Now.Year);
            help.AddOptions(this);

            return help.ToString();
        }
    }
}