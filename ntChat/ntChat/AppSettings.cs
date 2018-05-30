using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace ntChat
{
    public class AppSettings
    {
        private static AppSettings instance;
        private ISaveFile saveFile;
        public AppSettings()
        {
            saveFile = DependencyService.Get<ISaveFile>();
        }

        //////public TestClass TestClass { get; set; }

        public void Save()
        {
            using (var stream = saveFile.GetSaveFileStream(FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(AppSettings));
                serializer.Serialize(stream, instance);
            }
        }

        public Task SaveAsync()
        {
            return Task.Run(() => Save());
        }

        public static AppSettings Load()
        {
            AppSettings appSettings = null;
            var saveFile = DependencyService.Get<ISaveFile>();

            try
            {
                using (var stream = saveFile.GetSaveFileStream(FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(AppSettings));
                    appSettings = (AppSettings)serializer.Deserialize(stream);
                }
            }
            catch { appSettings = null; }

            if (appSettings == null)
                appSettings = new AppSettings();

            return appSettings;
        }

        public static Task<AppSettings> LoadAsync()
        {
            return Task.Run(() => Load());
        }

        public static AppSettings GetInstance()
        {
            if (instance == null)
                instance = Load();
            return instance;
        }

        public static Task<AppSettings> GetInstanceAsync()
        {
            return Task.Run(async () =>
            {
                if (instance == null)
                    instance = await LoadAsync();
                return instance;
            });
        }
    }
}
