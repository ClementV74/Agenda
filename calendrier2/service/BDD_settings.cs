//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace calendrier2.service
//{
//    public class BDD_settings
//    {
//        public void SaveSettings(string ServerName, string Username, string Password, string Database)
//        {
//            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//            config.AppSettings.Settings["host"].Value = ServerName;
//            config.AppSettings.Settings["user"].Value = Username;
//            config.AppSettings.Settings["password"].Value = Password;
//            config.Save(ConfigurationSaveMode.Modified);
//            ConfigurationManager.RefreshSection("appSettings");

//        }
//    }
//}
