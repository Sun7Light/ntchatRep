using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ntChat;
using Xamarin.Forms;

[assembly: Dependency(typeof(ntChat.Droid.DeviceName))]
namespace ntChat.Droid
{
    public class DeviceName : IDeviceName
    {
        public string GetEnvDeviceName()
        {
            return Build.Model;
        }
    }
}