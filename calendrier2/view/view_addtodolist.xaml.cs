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
            // Récupérer les valeurs des champs de saisie
            string titre = TB_Titre.Text;
            string description = TB_Description.Text;
            TimeOnly? heure = null;
            if (TimeOnly.TryParse(TB_Heure.Text, out TimeOnly parsedTime))
            {
                heure = parsedTime;
            }
            string lieu = TB_Lieux.Text;

            // Utiliser le DAO pour ajouter le nouvel événement à la base de données
            DAO_tache dao = new DAO_tache();

            // Recherchez la Todolist correspondante dans la base de données ou créez-en une nouvelle si elle n'existe pas
            Todolist todolist = dao.GetTodolistByTitle(titre);
            if (todolist == null)
            {
                // Si la Todolist n'existe pas, créez-en une nouvelle
                todolist = dao.AjouterTodolist(titre);
            }

            // Créer une nouvelle tâche associée à cette Todolist
            Tache nouvelleTache = new Tache
            {
                Description = description,
                Temps = heure,
                Lieux = lieu,
                TodolistIdtodolist = todolist.Idtodolist
            };

            // Ajouter la nouvelle tâche à la base de données
            dao.AjouterTache(nouvelleTache);

            // Rafraîchir ou actualiser l'interface utilisateur si nécessaire
            // Par exemple, effacer les champs de saisie ou afficher un message de confirmation
            TB_Titre.Text = "";
            TB_Description.Text = "";
            TB_Heure.Text = "";
            TB_Lieux.Text = "";

            // Vous pouvez également ajouter du code pour afficher un message de confirmation à l'utilisateur
            MessageBox.Show("Rappel ajouté avec succès !");
        }



    }
}
