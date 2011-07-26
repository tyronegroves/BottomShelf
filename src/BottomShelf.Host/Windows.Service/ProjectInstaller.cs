using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace BottomShelf.Host.Windows.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        public static bool DelayedAutoStart { get; set; }
        public static ServiceStartMode StartType { get; set; }
        public static ServiceAccount Account { get; set; }

        public static void Run(CommandLineParameters options)
        {
            var installUtilArguments = new List<string>();
            installUtilArguments.Add(Assembly.GetExecutingAssembly().Location);
            installUtilArguments.Add("/LogToConsole=true");
            DelayedAutoStart = options.DelayedAutoStart;

            ServiceAccount serviceAccount;
            Enum.TryParse(options.ServiceAccount, true, out serviceAccount);
            Account = serviceAccount;

            ServiceStartMode startType;
            Enum.TryParse(options.StartType, true, out startType);
            StartType = startType;

            if(options.UninstallWindowsService)
                installUtilArguments.Add("/u");

            ManagedInstallerClass.InstallHelper(installUtilArguments.ToArray());
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            serviceInstaller.DelayedAutoStart = DelayedAutoStart;
            serviceInstaller.StartType = StartType;
            serviceProcessInstaller.Account = Account;

            base.OnBeforeInstall(savedState);
        }
    }
}