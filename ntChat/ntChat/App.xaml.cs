using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace ntChat
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
            InitDefaultValues();
		}

		protected override void OnSleep ()
		{
            AppSettings.GetInstance().SaveAsync().Wait();
		}

		protected override void OnResume ()
		{
            InitDefaultValues();
		}

        private void InitDefaultValues()
        {
            AppSettings.GetInstanceAsync().ContinueWith(tsk =>
            {
                AppSettings appSettings = tsk.Result;

                if (appSettings.DefaultPort == default(int))
                    appSettings.DefaultPort = 224490;
                if (appSettings.ReceiveTimeout == default(int))
                    appSettings.ReceiveTimeout = 5000;
                if (appSettings.SendTimeout == default(int))
                    appSettings.SendTimeout = 5000;
                if (appSettings.BufferSize == default(int))
                    appSettings.BufferSize = 4096;
                if (appSettings.MaxMsgSize == default(int))
                    appSettings.MaxMsgSize = 1048576;
                if (appSettings.Backlog == default(int))
                    appSettings.Backlog = 100;

                if (string.IsNullOrWhiteSpace(appSettings.DeviceID))
                    appSettings.DeviceID = Guid.NewGuid().ToString("N");
                if (string.IsNullOrWhiteSpace(appSettings.DeviceName))
                    appSettings.DeviceName = DependencyService.Get<IDeviceName>().GetEnvDeviceName();
            });
        }
	}
}
