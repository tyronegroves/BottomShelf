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
            var options = new CommandLineOptions();

            if(!parser.ParseArguments(arguments, options))
                Environment.Exit(1);

            InstallOrUninstallWindowsServiceIfNecessary(options);

            var bottomShelfHost = new BottomShelfHost();

            if(Environment.UserInteractive)
            {
                bottomShelfHost.Start(arguments);
                WaitUntilUserWantsToExit();
                bottomShelfHost.Stop();

                Environment.Exit(0);
            }

            ServiceBase.Run(new BottomShelfService(bottomShelfHost));
        }

        private static void InstallOrUninstallWindowsServiceIfNecessary(CommandLineOptions options)
        {
            if(!options.InstallAsWindowService && !options.UninstallWindowsService) return;

            try
            {
                ProjectInstaller.Run(options);
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