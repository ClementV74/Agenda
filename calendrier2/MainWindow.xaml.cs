using calendrier2.contact_DB;
using calendrier2.service.DAO;
using calendrier2.view;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace calendrier2
{
    public partial class MainWindow : Window
    {

        private MqttClient mqttClient;

        public MainWindow()
        {
            InitializeComponent();


            LoadCredentials(); // Charger les informations d'identification sauvegardées

            try { 
            mqttClient = new MqttClient("172.31.146.49"); // Adresse IP du broker MQTT
            string clientId = Guid.NewGuid().ToString();
            // Set username and password
            mqttClient.Connect(clientId, "clement", "clem");

            // Subscribe to topics
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
            mqttClient.Subscribe(new string[] { "authentification" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
                catch (Exception ex)
            {
                    MessageBox.Show($"Erreur lors de la connexion au serveur MQTT : {ex.Message}");
                }
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Message);
            if (e.Topic == "authentification" && message == "ok")
            {
                Dispatcher.Invoke(() =>
                {
                    Ecran.Children.Clear();
                    var dashboardView = new view_dashboard();
                    Grid.SetColumnSpan(dashboardView, 2);
                    Ecran.Children.Add(dashboardView);
                });
            }

            if (e.Topic == "authentification" && message == "off")
            {
                MessageBox.Show("Accès refusé carte invalide", "Erreur d'authentification", MessageBoxButton.OK, MessageBoxImage.Error);
                //ajoute un delais de 3 sec
                System.Threading.Thread.Sleep(3000);
            }
        }



        private void BTN_Login_Click(object sender, RoutedEventArgs e)
        {
            string username = TB_Username.Text;
            string password = TB_Password.Password;

            DAO_Utilisateur daoUtilisateur = new DAO_Utilisateur(new ContactContext()); // Créer une instance de DAO_Utilisateur

            if (daoUtilisateur.VerifyCredentials(username, password))
            {
                // Les informations d'identification sont correctes, redirigez l'utilisateur vers le tableau de bord avec une animation
                ShowDashboardView(); // Afficher le tableau de bord

                // Sauvegarder automatiquement les informations d'identification
                SaveCredentials(username, password);


            }
            else
            {


                // Affiche un message d'erreur
                MessageBox.Show("Accès refusé. Nom d'utilisateur ou mot de passe incorrect.", "Erreur d'authentification", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void SaveCredentials(string username, string password)
        {
            // Accéder au fichier de configuration intégré en tant que ressource
            string configFile = "Ressources.config.txt";

            // Écriture des informations d'identification dans le fichier de configuration
            using (StreamWriter writer = new StreamWriter(configFile))
            {
                writer.WriteLine(username);
                writer.WriteLine(password);
            }
        }

        private void LoadCredentials()
        {
            // Accéder au fichier de configuration intégré en tant que ressource
            string configFile = "Ressources.config.txt";

            // Vérification de l'existence du fichier de configuration
            if (File.Exists(configFile))
            {
                // Lecture des informations d'identification depuis le fichier de configuration
                string[] lines = File.ReadAllLines(configFile);
                if (lines.Length >= 2)
                {
                    TB_Username.Text = lines[0];
                    TB_Password.Password = lines[1];
                }
            }
        }

        private void ShowDashboardView()
        {
            // Déclencher l'animation de transition vers le tableau de bord
            Storyboard fadeOutStoryboard = Resources["FadeIn"] as Storyboard;
            fadeOutStoryboard.Begin(Ecran);


            Ecran.Children.Clear();
            var dashboardView = new view_dashboard();
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran.Children.Add(dashboardView);



        }

        private void enter_press(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BTN_Login_Click(sender, e);
            }
        }

        private void Register_Click(object sender, MouseButtonEventArgs e)
        {
            var registerView = new view_register(new ContactContext());

            Ecran.Children.Clear();
            Grid.SetColumnSpan(registerView, 2);
            Ecran.Children.Add(registerView);
        }
    }
}
