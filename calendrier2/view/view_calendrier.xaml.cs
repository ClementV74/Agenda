using calendrier2.service;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
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
            List<string> eventDescriptions = new List<string>();

            // Vérifier s'il y a des événements à afficher
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string description = eventItem.Summary;
                    if (eventItem.Start.DateTime != null)
                    {
                        description += $" - {eventItem.Start.DateTime}";
                    }
                    else if (eventItem.Start.Date != null)
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



        private async void DisplayCurrentMonth()
        {
            MonthTextBlock.Text = _currentMonth.ToString("MMMM yyyy");

            try
            {
                Events events = await GetEventsForMonthAsync(_currentMonth);
                DisplayEvents(events);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des événements : " + ex.Message);
            }
        }

        private async Task<Events> GetEventsForMonthAsync(DateTime month)
        {
            DateTime startOfMonth = new DateTime(month.Year, month.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            EventsResource.ListRequest request = _calendarService.Events.List("primary");
            request.TimeMin = startOfMonth;
            request.TimeMax = endOfMonth;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return await request.ExecuteAsync();
        }

        private void AfficherEvenements()
        {
            // Définir les paramètres de la requête pour récupérer les événements
            EventsResource.ListRequest request = _calendarService.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // Récupérer les événements à partir de Google Calendar
            Events events = request.Execute();

            // Afficher les événements dans le ListBox
            if (events.Items != null && events.Items.Count > 0)
            {
                List<string> eventDescriptions = new List<string>();
                foreach (var eventItem in events.Items)
                {
                    string description = eventItem.Summary;
                    if (eventItem.Start.DateTime != null)
                    {
                        description += $" - {eventItem.Start.DateTime}";
                    }
                    else if (eventItem.Start.Date != null)
                    {
                        description += $" - {eventItem.Start.Date}";
                    }
                    eventDescriptions.Add(description);
                }
                EventsListBox.ItemsSource = eventDescriptions;
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

        public void AuthentifierEtObtenirEvenements()
        {
            UserCredential credential;

            // Charger les identifiants du client
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Créer le service Google Calendar
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Récupérer les événements du calendrier
            Events events = service.Events.List("primary").Execute();
            Console.WriteLine("Prochains événements:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    Console.WriteLine("{0} ({1})", eventItem.Summary, eventItem.Start?.DateTime);
                }
            }
            else
            {
                Console.WriteLine("Aucun événement trouvé.");
            }
        }

        private void ConnectGoogleCalendarButton_Click(object sender, RoutedEventArgs e)
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
