using System.ServiceProcess;

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

        protected override void OnStart(string[] args)
        {
            bottomShelfHost.Start(args);
        }

        protected override void OnStop()
        {
            bottomShelfHost.Stop();
        }
    }
}