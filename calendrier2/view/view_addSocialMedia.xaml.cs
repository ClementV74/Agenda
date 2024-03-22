using calendrier2.contact_DB;
using System;
using System.Collections.Generic;
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

namespace calendrier2.view
{
    /// <summary>
    /// Logique d'interaction pour view_addSocialMedia.xaml
    /// </summary>
    /// 

    public partial class view_addSocialMedia : UserControl
    {
        private ContactContext _dbContext = new ContactContext(); // Contexte de la base de données

        public view_addSocialMedia()
        {
            InitializeComponent();
          

        }

        private void BTN_Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void BTN_Calendrier_Click(object sender, RoutedEventArgs e)
        {
            var calendrierview = new view_calendrier(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord
            Ecran_AddReseau.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(calendrierview, 2);
            Ecran_AddReseau.Children.Add(calendrierview);
        }

        

        private void BTN_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard();
            Ecran_AddReseau.Children.Clear();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran_AddReseau.Children.Add(dashboardView);
        }

        private void BTN_AddReseau_Click(object sender, RoutedEventArgs e)
        {
            // Ajouter un nouveau réseau social
            // Récupérer les informations saisies par l'utilisateur juste le nom du reseau social
            string nomReseau = TB_NomReseau.Text;
            // Créer un nouvel objet ReseauxSociauxList 
            ReseauxSociauxList reseau = new ReseauxSociauxList { NomReseau = nomReseau };
            // Ajouter le nouvel objet au contexte
            _dbContext.ReseauxSociauxLists.Add(reseau);
            // Enregistrer les modifications dans la base de données
            _dbContext.SaveChanges();
            // Afficher un message de confirmation
            MessageBox.Show("Réseau social ajouté avec succès !");
        }
        private void BTN_retours_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard();
            Ecran_AddReseau.Children.Clear();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran_AddReseau.Children.Add(dashboardView);
        }

        private void Button_Contact_Click(object sender, RoutedEventArgs e)
        {
            var contactview = new view_contact(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord

            // Remplace le contenu des deux parties de la grille
            Ecran_AddReseau.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(contactview, 2);
            Ecran_AddReseau.Children.Add(contactview);
        }
    }
}
