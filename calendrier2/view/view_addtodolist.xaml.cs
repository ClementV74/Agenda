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
            var contactview = new view_contact();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(contactview, 2);
            Ecran_Contact.Children.Add(contactview);

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
            string titre = TB_Titre.Text;
            string description = TB_Description.Text;
            TimeOnly? heure;

            // Essayer de convertir le texte de la TextBox en un objet DateTime
            if (TimeOnly.TryParse(TB_Heure.Text, out TimeOnly heureSaisie))
            {
                // La conversion a réussi, utilisez la valeur convertie
                heure = heureSaisie;
            }
            else
            {
                // La conversion a échoué, vous pouvez gérer cette situation en affichant un message à l'utilisateur
                MessageBox.Show("Format d'heure invalide. Veuillez saisir une heure valide au format HH:mm:ss");
                return; // Sortir de la méthode sans rien faire de plus
            }

            string lieu = TB_Lieux.Text;

            // Créer une instance de DAO_tache
            DAO_tache daoTache = new DAO_tache();

            // Appeler la méthode AjouterEvenement
            daoTache.AjouterEvenement(titre, description, heure, lieu);

            //retourner sur la page todolist
            var contactview = new view_contact();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(contactview, 2);
            Ecran_Contact.Children.Add(contactview);
        }





    }
}
