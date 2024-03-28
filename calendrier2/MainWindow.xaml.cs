using calendrier2.contact_DB;
using calendrier2.service.DAO;
using calendrier2.view;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.RoutedEventArgs;


namespace calendrier2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void BTN_Login_Click(object sender, RoutedEventArgs e)
        {
            string username = TB_Username.Text;
            string password = TB_Password.Password;



          

            DAO_Utilisateur daoUtilisateur = new DAO_Utilisateur(new ContactContext());

            if (daoUtilisateur.VerifyCredentials(username, password))
            {
                // Les informations d'identification sont correctes, redirigez l'utilisateur vers le tableau de bord
                var dashboardView = new view_dashboard();
                Ecran.Children.Clear();
                Grid.SetColumnSpan(dashboardView, 2);
                Ecran.Children.Add(dashboardView);
            }
            else
            {
                // Affiche un message d'erreur
                MessageBox.Show("Accès refusé. Nom d'utilisateur ou mot de passe incorrect.", "Erreur d'authentification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void enter_press(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BTN_Login_Click(sender, e);
            }
        }


        private void Register_Click(object sender, MouseButtonEventArgs e)
        {
            var registerView = new view_register(new ContactContext());
            Ecran.Children.Clear();
            Grid.SetColumnSpan(registerView, 2);
            Ecran.Children.Add(registerView);
        }


    }
}


