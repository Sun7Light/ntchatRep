using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(ntChat.Droid.SaveFile))]
namespace ntChat.Droid
{
    public class SaveFile: ISaveFile
    {
        public Stream GetSaveFileStream(FileMode fileMode)
        {
            string savePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "save");

            FileStream fs = File.Open(savePath, fileMode, FileAccess.ReadWrite, FileShare.None);

            return fs;
        }
    }
}
