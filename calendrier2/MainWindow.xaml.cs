using calendrier2.contact_DB;
using calendrier2.service.DAO;
using calendrier2.view;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace calendrier2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BTN_Login_Click(object sender, RoutedEventArgs e)
        {
            string username = TB_Username.Text;
            string password = TB_Password.Password;

            DAO_Utilisateur daoUtilisateur = new DAO_Utilisateur(new ContactContext());

            if (daoUtilisateur.VerifyCredentials(username, password))
            {
                // Les informations d'identification sont correctes, redirigez l'utilisateur vers le tableau de bord avec une animation
                ShowDashboardView();
            }
            else
            {
                // Affiche un message d'erreur
                MessageBox.Show("Accès refusé. Nom d'utilisateur ou mot de passe incorrect.", "Erreur d'authentification", MessageBoxButton.OK, MessageBoxImage.Error);
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
            Ecran.Children.Add(registerView);
        }
    }
}
