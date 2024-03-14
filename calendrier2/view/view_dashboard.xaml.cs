using calendrier2;
using calendrier2.service.DAO;
using System.Text;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using calendrier2.contact_DB;

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
                var rappels = daoTache.GetRappels();

                StringBuilder sb = new StringBuilder();
                foreach (var rappel in rappels)
                {
                    string etatTache = rappel.Fait == true ? "✔" : "❌"; // Utilisation de symboles pour représenter l'état de la tâche
                    sb.AppendLine($"Tâche: {rappel.TodolistIdtodolistNavigation?.Name} - {etatTache} - Heure: {rappel.Temps}");
                }

                txtRappels.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors du chargement des rappels : {ex.Message}");
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

    


    }
}

