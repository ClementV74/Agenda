using calendrier2.view;
using calendrier2;
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
    /// Logique d'interaction pour view_calendrier.xaml
    /// </summary>
    public partial class view_calendrier : UserControl
    {
        public view_calendrier()
        {
            InitializeComponent();
            // Abonnez-vous à l'événement ScriptErrorsSuppressedChanged pour intercepter les erreurs de script
            webBrowser.Navigating += WebBrowser_Navigating;
        }
        private void WebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {


        }

        private void BTN_Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();

        }

        private void BTN_Calendrier_Click(object sender, RoutedEventArgs e)
        {
            var calendrierview = new view_calendrier(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord
            Ecran_Calendar.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(calendrierview, 2);
            Ecran_Calendar.Children.Add(calendrierview);
        }



        private void BTN_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord

            // Remplace le contenu des deux parties de la grille
            Ecran_Calendar.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(dashboardView, 2);
            Ecran_Calendar.Children.Add(dashboardView);
        }

        private void BTN_Contact_Click(object sender, RoutedEventArgs e)
        {
            var contactview = new view_contact(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord

            // Remplace le contenu des deux parties de la grille
            Ecran_Calendar.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(contactview, 2);
            Ecran_Calendar.Children.Add(contactview);

        }
    }
}
