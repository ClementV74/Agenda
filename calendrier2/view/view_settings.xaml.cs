using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace calendrier2.view
{
    /// <summary>
    /// Logique d'interaction pour view_settings.xaml
    /// </summary>
    public partial class view_settings : UserControl
    {
        public view_settings()
        {
            InitializeComponent();
        }


        private void BTN_DeconnexionGoogle_Click(object sender, RoutedEventArgs e)
        {
            // Révoquer l'accès au calendrier Google
            try
            {
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets { ClientId = "357616497397-6ta0ili2bh2129s3rbpj7fpjvftq5t35.apps.googleusercontent.com", ClientSecret = "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" },
                    new[] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("Calendar.Auth.Store")).Result;

                // Supprimer les informations d'identification enregistrées
                credential.RevokeTokenAsync(CancellationToken.None).Wait();

                MessageBox.Show("Déconnexion réussie de Google Calendar.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de la déconnexion de Google Calendar : {ex.Message}");
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

        private void BTN_Database_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo // Ouvrir phpMyAdmin
                {
                    FileName = "https://phpmyadmin.alwaysdata.com/", 
                    UseShellExecute = true // Ouvrir dans le navigateur par défaut
                };
                Process.Start(psi); // Ouvrir la page
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de l'ouverture de la page Google : {ex.Message}"); // Affichez un message d'erreur si une exception est levée
            }

       

      
    }

        private void BTN_Reseau_Click(object sender, RoutedEventArgs e)
        {
            var reseauview = new view_addSocialMedia();
            Ecran_Principale.Children.Clear();
            Grid.SetColumnSpan(reseauview, 2);
            Ecran_Principale.Children.Add(reseauview);
        }
    }
    }






