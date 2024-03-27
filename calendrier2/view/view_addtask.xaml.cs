using calendrier2.contact_DB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace calendrier2.view
{
    public partial class view_addtask : UserControl
    {
        public DAO_tache daoTache = new DAO_tache(); // Assurez-vous d'avoir une instance de DAO_tache

        public view_addtask()
        {
            InitializeComponent();
            LoadTodolists(); // Chargez les Todolists dans la ListBox lors de l'initialisation de la vue
        }

        private void LoadTodolists()
        {
            try
            {
                using (var context = new ContactContext())
                {
                    // Récupérer toutes les Todolists depuis la base de données et les lier à la ListBox
                    ListBox_Todolists.ItemsSource = context.Todolists.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors du chargement des Todolists : {ex.Message}");
            }
        }

        private void BTN_AddRappel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Récupérer les informations de la vue
                string description = TB_Description.Text;
                string lieu = TB_Lieux.Text;

                // Récupérer l'élément sélectionné dans la ListBox (Todolist)
                Todolist selectedTodolist = (Todolist)ListBox_Todolists.SelectedItem;

                // Vérifier si une Todolist est sélectionnée
                if (selectedTodolist == null)
                {
                    MessageBox.Show("Veuillez sélectionner une Todolist.");
                    return;
                }

                // Créer une nouvelle tâche
                Tache newTask = new Tache
                {
                    Description = description,
                    Lieux = lieu,
                    TodolistIdtodolist = selectedTodolist.Idtodolist // Affecter l'ID de la Todolist sélectionnée à la nouvelle tâche
                };

                // Ajouter la nouvelle tâche à la base de données
                using (var context = new ContactContext())
                {
                    context.Taches.Add(newTask);
                    context.SaveChanges();
                }

                // Afficher un message de succès
                MessageBox.Show("Tâche ajoutée avec succès.");

                // Recharger la vue de contact après l'ajout de la tâche
                var contactview = new view_contact();
                Ecran_Principale.Children.Clear();
                Grid.SetColumnSpan(contactview, 2);
                Ecran_Principale.Children.Add(contactview);
            }
            catch (Exception ex)
            {
                // Gérer les exceptions
                MessageBox.Show($"Une erreur s'est produite lors de l'ajout de la tâche : {ex.Message}");
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

        private void Button_Retour_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard();
            Ecran_Principale.Children.Clear();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran_Principale.Children.Add(dashboardView);
        }

        private void BTN_Dashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_retour_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
