using calendrier2.contact_DB;
using calendrier2.service.DAO;
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
    /// Logique d'interaction pour view_addtodolist.xaml
    /// </summary>
    public partial class view_addtodolist : UserControl
    {
        public view_addtodolist()
        {
            InitializeComponent();

        }

        private void BTN_retour_Click(object sender, RoutedEventArgs e) 
        {
            var dashboardView = new view_dashboard();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran_Contact.Children.Add(dashboardView);

        }

        private void BTN_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran_Contact.Children.Add(dashboardView);
        }

        private void BTN_Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void BTN_Calendrier_Click(object sender, RoutedEventArgs e)
        {
            var calendrierview = new view_calendrier();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(calendrierview, 2);
            Ecran_Contact.Children.Add(calendrierview);
        }


        private void BTN_AddRappel_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les informations des TextBox
            string description = TB_Description.Text;
            TimeOnly? temps;

            // Essayer de convertir le texte de la TextBox en un objet TimeOnly
            if (!TimeOnly.TryParse(TB_Heure.Text, out TimeOnly heure))
            {
                // La conversion a échoué, afficher un message à l'utilisateur et quitter la méthode
                MessageBox.Show("Format d'heure invalide. Veuillez saisir une heure valide au format HH:mm:ss");
                return;
            }
            else
            {
                // La conversion a réussi, utiliser la valeur convertie
                temps = heure;
            }

            string lieu = TB_Lieux.Text;

            try
            {
                // Créer une instance de la classe Tache et ajouter la nouvelle tâche à la base de données
                Tache nouvelleTache = new Tache
                {
                    Description = description,
                    Temps = temps,
                    Lieux = lieu
                    // Ajoutez d'autres propriétés si nécessaire
                };

                // Ajouter la nouvelle tâche en utilisant votre DAO
                DAO_tache daoTache = new DAO_tache();
                daoTache.AjouterTache(nouvelleTache);

                // Rediriger l'utilisateur vers la vue view_contact
                var contactview = new view_contact();
                Ecran_Contact.Children.Clear();
                Grid.SetColumnSpan(contactview, 2);
                Ecran_Contact.Children.Add(contactview);
            }
            catch (Exception ex)
            {
                // Gérer toute exception qui pourrait survenir lors de l'ajout de la tâche
                MessageBox.Show($"Une erreur s'est produite lors de l'ajout de la tâche : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }






    }
}
