using calendrier2.service;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace calendrier2.view
{
    public partial class view_calendrier : UserControl
    {
        private CalendarService _calendarService;
        private DateTime _currentMonth;

        public view_calendrier()
        {
            InitializeComponent();

            // Obtenez le service Google Calendar autorisé
            _calendarService = GoogleCalendarAuth.GetCalendarService();

            // Chargez et affichez les événements
            AfficherEvenements();
            _currentMonth = DateTime.Today;
            DisplayCurrentMonth();
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            _currentMonth = _currentMonth.AddMonths(-1);
            DisplayCurrentMonth();
        }

        private void BTN_Suivant_Click(object sender, RoutedEventArgs e)
        {
            _currentMonth = _currentMonth.AddMonths(1);
            DisplayCurrentMonth();
        }
        private void DisplayEvents(Events events)
        {
            // Créer une nouvelle liste pour stocker les descriptions des événements
            ObservableCollection<string> eventDescriptions = new ObservableCollection<string>();

            // Vérifier s'il y a des événements à afficher
            if (events.Items != null && events.Items.Count > 0) // S'il y a des événements à afficher
            {
                foreach (var eventItem in events.Items) // Parcourir les événements
                {
                    string description = eventItem.Summary; // Description de l'événement
                    if (eventItem.Start.DateTime != null)
                    {
                        description += $" - {eventItem.Start.DateTime}";
                    }
                    else if (eventItem.Start.Date != null) // Si seule la date de début est définie
                    {
                        description += $" - {eventItem.Start.Date}";
                    }
                    eventDescriptions.Add(description);
                }
            }
            else
            {
                eventDescriptions.Add("Aucun événement trouvé.");
            }

            // Affecter la nouvelle liste à la source de données de la liste box
            EventsListBox.ItemsSource = eventDescriptions;
        }



        private async void DisplayCurrentMonth() // Afficher le mois actuel
        {
            MonthTextBlock.Text = _currentMonth.ToString("MMMM yyyy"); // Afficher le mois et l'année actuels

            try
            {
                Events events = await GetEventsForMonthAsync(_currentMonth); // Récupérer les événements pour le mois actuel
                DisplayEvents(events); // Afficher les événements
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des événements : " + ex.Message);
            }
        }

        private async Task<Events> GetEventsForMonthAsync(DateTime month) // Récupérer les événements pour un mois donné
        {
            DateTime startOfMonth = new DateTime(month.Year, month.Month, 1); // Premier jour du mois
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Dernier jour du mois

            EventsResource.ListRequest request = _calendarService.Events.List("primary"); // primary = calendrier par défaut
            request.TimeMin = startOfMonth; // Date minimale
            request.TimeMax = endOfMonth; // Date maximale
            request.SingleEvents = true; // Événements uniques
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime; // Trier par date de début

            return await request.ExecuteAsync(); // Exécuter la requête de manière asynchrone
        }

        private void AfficherEvenements() // Afficher les événements à partir de Google Calendar
        {
            // Définir les paramètres de la requête pour récupérer les événements
            EventsResource.ListRequest request = _calendarService.Events.List("primary"); // primary = calendrier par défaut
            request.TimeMin = DateTime.Now; // Date minimale
            request.ShowDeleted = false; // Ne pas afficher les événements supprimés
            request.SingleEvents = true; // Événements uniques
            request.MaxResults = 10; // Nombre maximum d'événements à récupérer
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // Récupérer les événements à partir de Google Calendar
            Events events = request.Execute();

            // Afficher les événements dans le ListBox
            if (events.Items != null && events.Items.Count > 0) // S'il y a des événements à afficher
            {
                List<string> eventDescriptions = new List<string>(); // Créer une nouvelle liste pour stocker les descriptions des événements
                foreach (var eventItem in events.Items) // Parcourir les événements
                {
                    string description = eventItem.Summary; // Description de l'événement
                    if (eventItem.Start.DateTime != null) // Si la date et l'heure de début sont définies
                    {
                        description += $" - {eventItem.Start.DateTime}"; // Ajouter la date et l'heure de début à la description
                    }
                    else if (eventItem.Start.Date != null) // Si seule la date de début est définie
                    {
                        description += $" - {eventItem.Start.Date}"; // Ajouter la date de début à la description
                    }
                    eventDescriptions.Add(description); // Ajouter la description de l'événement à la liste
                }
                EventsListBox.ItemsSource = eventDescriptions; // Affecter la liste des descriptions des événements à la source de données de la liste box
            }
            else
            {
                EventsListBox.Items.Add("Aucun événement trouvé."); 
            }
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
            Ecran_Calendar.Children.Clear();
            Grid.SetColumnSpan(calendrierview, 2);
            Ecran_Calendar.Children.Add(calendrierview);
        }

        private void BTN_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard();
            Ecran_Calendar.Children.Clear();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran_Calendar.Children.Add(dashboardView);
        }

        private void BTN_Contact_Click(object sender, RoutedEventArgs e)
        {
            var contactView = new view_contact();
            Ecran_Calendar.Children.Clear();
            Grid.SetColumnSpan(contactView, 2);
            Ecran_Calendar.Children.Add(contactView);
        }

        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Calendrier";

        public void AuthentifierEtObtenirEvenements() // Authentifier l'utilisateur et obtenir les événements du calendrier
        {
            

            // Créer le service Google Calendar
            var service = new CalendarService(new BaseClientService.Initializer()
            {
              
                ApplicationName = ApplicationName,
            });

            // Récupérer les événements du calendrier
            Events events = service.Events.List("primary").Execute();
            Console.WriteLine("Prochains événements:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    Console.WriteLine("{0} ({1})", eventItem.Summary, eventItem.Start?.DateTime); // Afficher le nom et la date de début de l'événement
                }
            }
            else
            {
                Console.WriteLine("Aucun événement trouvé.");
            }
        }

        private void ConnectGoogleCalendarButton_Click(object sender, RoutedEventArgs e) // Bouton de connexion à Google Calendar
        {
            try
            {
                // Obtenez le service Google Calendar autorisé
                var calendarService = GoogleCalendarAuth.GetCalendarService();

                // L'authentification a réussi
                MessageBox.Show("Authentification réussie !");
            }
            catch (Exception ex)
            {
                // Gérez les erreurs d'authentification
                MessageBox.Show("Erreur d'authentification : " + ex.Message);
            }
        }
    }
}
