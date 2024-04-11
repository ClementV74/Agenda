using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace calendrier2.service
{
    public class BDD_modifier
    {
        public void ModifierConnexionBDD(string host, string port, string user, string password, string database)
        {
            // Charge la configuration de l'application
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Supprime les anciennes valeurs
            config.AppSettings.Settings.Remove("host");
            config.AppSettings.Settings.Remove("port");
            config.AppSettings.Settings.Remove("user");
            config.AppSettings.Settings.Remove("password");
            config.AppSettings.Settings.Remove("database");

            // Ajoute les nouvelles valeurs
            config.AppSettings.Settings.Add("host", host);
            config.AppSettings.Settings.Add("port", port);
            config.AppSettings.Settings.Add("user", user);
            config.AppSettings.Settings.Add("password", password);
            config.AppSettings.Settings.Add("database", database);

            // Sauvegarde les modifications
            config.Save(ConfigurationSaveMode.Modified);

            // Rafraîchit la section appSettings
            ConfigurationManager.RefreshSection("appSettings");
        }






    }
}
