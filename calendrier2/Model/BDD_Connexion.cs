using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendrier2.Model
{
    public class BDD_Connexion
    {
        public string ServerName { get; set; }
        public string Username { get; set; }

        public string Port { get; set; }
            
        public string Password { get; set; }
        public string Database { get; set; }

        public string mysqlVer { get; set; }

        public string ConString { get { return string.Format("server={0};port={1};user={2};password={3};database={4}", ServerName, Port, Username, Password, Database); } }


        
            public BDD_Connexion()
            {
                ServerName = ConfigurationManager.AppSettings["host"];
                Port = ConfigurationManager.AppSettings["port"];
                Username = ConfigurationManager.AppSettings["user"];
                Password = ConfigurationManager.AppSettings["password"];
                Database = ConfigurationManager.AppSettings["database"];
                mysqlVer = ConfigurationManager.AppSettings["mysqlVer"];
            }

   
        public void SaveSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["host"].Value = ServerName;
            config.AppSettings.Settings["user"].Value = Username;
            config.AppSettings.Settings["password"].Value = Password;
            config.AppSettings.Settings["database"].Value = Database;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

        }
    }
}
