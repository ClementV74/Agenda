using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using calendrier2.service;

namespace calendrier2.view
{
    /// <summary>
    /// Logique d'interaction pour view_reglageBDD.xaml
    /// </summary>
    
    public partial class view_reglageBDD : UserControl
    {
       
        public view_reglageBDD()
        {
            InitializeComponent();
            TB_Database.Text = ConfigurationManager.AppSettings["database"];
            TB_Host.Text = ConfigurationManager.AppSettings["host"];
            TB_Password.Text = ConfigurationManager.AppSettings["password"];
           
            TB_User.Text = ConfigurationManager.AppSettings["user"];


        }

        private void BTN_Save_Click(object sender, RoutedEventArgs e)
        {
            string ServerName = TB_Host.Text;
            string Username = TB_User.Text;
            string Password = TB_Password.Text;
            string Database = TB_Database.Text;
      


            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["host"].Value = ServerName;
            config.AppSettings.Settings["user"].Value = Username;
            config.AppSettings.Settings["password"].Value = Password;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            Ecran.Children.Clear();
            var dashboardView = new view_dashboard();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran.Children.Add(dashboardView);

        }

      
    }
}

