using System;
using System.ServiceProcess;
using BottomShelf.Host.Windows.Service;
using CommandLine;

namespace BottomShelf.Host
{
    internal class Program
    {
        private static void Main(string[] arguments)
        {
            var parser = new CommandLineParser(new CommandLineParserSettings(Console.Error));
            var commandLineParameters = new CommandLineParameters();

            if(!parser.ParseArguments(arguments, commandLineParameters))
                Environment.Exit(1);

            InstallOrUninstallWindowsServiceIfNecessary(commandLineParameters);

            var bottomShelfHost = new BottomShelfHost();

            if(Environment.UserInteractive)
            {
                bottomShelfHost.Start(commandLineParameters);
                WaitUntilUserWantsToExit();
                bottomShelfHost.Stop();

                Environment.Exit(0);
            }

            ServiceBase.Run(new BottomShelfService(bottomShelfHost));
        }

        private static void InstallOrUninstallWindowsServiceIfNecessary(CommandLineParameters commandLineParameters)
        {
            if(!commandLineParameters.InstallAsWindowService && !commandLineParameters.UninstallWindowsService) return;

            try
            {
                ProjectInstaller.Run(commandLineParameters);
                Environment.Exit(0);
            }
            catch
            {
                Environment.Exit(1);
            }
        }

        private static void WaitUntilUserWantsToExit()
        {
            Console.WriteLine("Press CTRL+Z to exit");
            if(Console.ReadLine() == null)
                return;

            WaitUntilUserWantsToExit();
        }
    }
}