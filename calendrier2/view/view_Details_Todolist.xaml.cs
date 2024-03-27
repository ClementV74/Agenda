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
    /// Logique d'interaction pour view_Details_Todolist.xaml
    /// </summary>
    public partial class view_Details_Todolist : UserControl
    {
        public DAO_tache daoTache = new DAO_tache(); // Assurez-vous d'avoir une instance de DAO_tache
        private Todolist selectedCategory;
        public view_Details_Todolist()
        {
            InitializeComponent();
          

        }



        public void AfficherRappels(int selectedTodolistId)
        {
            try
            {
                var rappels = daoTache.GetInfo().Where(t => t.TodolistIdtodolist == selectedTodolistId).ToList(); // Filtrer les tâches en fonction de l'ID de la todolist sélectionnée
                lstRappels.ItemsSource = rappels; // Liaison des données filtrées à la ListView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors du chargement des rappels : {ex.Message}");
            }
        }



        private void BTN_Supp_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            // Obtenez l'élément de données (rappel) associé à ce bouton
            Tache rappel = (Tache)button.DataContext;

            // Obtenez l'ID de la liste de tâches associée au rappel
            int selectedTodolistId = rappel.TodolistIdtodolist;

            // Supprimez cet élément de la liste
            daoTache.SupprimerEvenement(rappel.Idtache);

            // Appelez AfficherRappels avec l'ID de la liste de tâches
            AfficherRappels(selectedTodolistId);
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

        private void Button_Retour_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard();
            Ecran_Principale.Children.Clear();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran_Principale.Children.Add(dashboardView);
        }

        private void Button_addTaskClick(object sender, RoutedEventArgs e)
        {
            var addtaskview = new view_addtask(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord
            Ecran_Principale.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(addtaskview, 2);
            Ecran_Principale.Children.Add(addtaskview);

        }

        
    }
}


