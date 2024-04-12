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
using calendrier2.Model;
using calendrier2.service;
using calendrier2.service.DAO;


namespace calendrier2.view
{
    /// <summary>
    /// Logique d'interaction pour view_reglageBDD.xaml
    /// </summary>

    public partial class view_reglageBDD : UserControl
    {
        BDD_Connexion bDD_Connexion;
        public view_reglageBDD()
        {
            InitializeComponent();

            bDD_Connexion = new BDD_Connexion();
         
            TB_Database.Text = ConfigurationManager.AppSettings["database"];
            TB_Host.Text = ConfigurationManager.AppSettings["host"];
            TB_Password.Text = ConfigurationManager.AppSettings["password"];
            TB_User.Text = ConfigurationManager.AppSettings["user"];


        }

        private void BTN_Save_Click(object sender, RoutedEventArgs e)
        {
            bDD_Connexion.ServerName = TB_Host.Text;
            bDD_Connexion.Username = TB_User.Text;
            bDD_Connexion.Password = TB_Password.Text;
            bDD_Connexion.Database = TB_Database.Text;

        
            bDD_Connexion.SaveSettings();


          
            Ecran.Children.Clear();
            var dashboardView = new view_dashboard();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran.Children.Add(dashboardView);

        }

      
    }
}

