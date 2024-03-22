using calendrier2;
using calendrier2.service.DAO;
using System.Text;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using calendrier2.contact_DB;
using System.Windows.Input;

namespace calendrier2.view
{
    /// <summary>
    /// Logique d'interaction pour view_dashboard.xaml
    /// </summary>
    /// 

    public partial class view_dashboard : UserControl
    {
        public DAO_tache daoTache = new DAO_tache(); // Assurez-vous d'avoir une instance de DAO_tache

        public view_dashboard()
        {
            InitializeComponent();
            AfficherRappels();


        }

       


        public void AfficherRappels()
        {
            try
            {
                var rappels = daoTache.GetRappels(); // Appel de la méthode dans votre DAO pour récupérer les rappels

                lstRappels.ItemsSource = rappels; // Liaison des données à la ListView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors du chargement des rappels : {ex.Message}"); // Affichez un message d'erreur si une exception est levée
            }
        }




        private void BTN_Contact_Click(object sender, RoutedEventArgs e)
        {
            var contactview = new view_contact(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord

            // Remplace le contenu des deux parties de la grille
            Ecran_Principale.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(contactview, 2);
            Ecran_Principale.Children.Add(contactview);
        }

        private void BTN_Calendrier_Click(object sender, RoutedEventArgs e)
        {
            var calendrierview = new view_calendrier(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord
            Ecran_Principale.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(calendrierview, 2);
            Ecran_Principale.Children.Add(calendrierview);

        }

        private void BTN_Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();

        }

        private void Button_addRappelClick(object sender, RoutedEventArgs e)
        {
            var addtodolistview = new view_addtodolist(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord
            Ecran_Principale.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(addtodolistview, 2);
            Ecran_Principale.Children.Add(addtodolistview);
        }

        private void BTN_Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsview = new view_settings(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord
            Ecran_Principale.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(settingsview, 2);
            Ecran_Principale.Children.Add(settingsview);
        }
    }
}

