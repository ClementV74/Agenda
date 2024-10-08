﻿using calendrier2.contact_DB;
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
    /// Logique d'interaction pour view_DetailUser.xaml
    /// </summary>
    public partial class view_DetailUser : UserControl
    {
        private Contact _selectedContact;
        private List<ContactReseauxSociaux> _reseauxSociaux;
        private DAO_Reseaux _daoReseaux; // Ajout d'un champ pour stocker l'instance de DAO_Reseaux

        public view_DetailUser(Contact selectedContact)
        {
            InitializeComponent();

            _selectedContact = selectedContact; // Récupérer le contact sélectionné

            NomTextBox.Text = selectedContact.Nom; // Afficher les informations du contact dans les TextBox
            PrenomTextBox.Text = selectedContact.Prenom;
            EmailTextBox.Text = selectedContact.Email;
            TelTextBox.Text = selectedContact.Tel;
            StatusBox.Text = selectedContact.Status;

            // Instancier DAO_Reseaux
            _daoReseaux = new DAO_Reseaux();

            // Récupérer et afficher les réseaux sociaux du contact sélectionné
            _reseauxSociaux = _daoReseaux.GetContactReseauxSociauxByContact(_selectedContact);
            DataGridReseaux.ItemsSource = _reseauxSociaux;
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


        private void BTN_retour_Click(object sender, RoutedEventArgs e)
        {

            var daoReseaux = new DAO_Reseaux();
            daoReseaux.SaveChangesAndUpdateReseaux(_reseauxSociaux);

            var contactview = new view_contact();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(contactview, 2);
            Ecran_Contact.Children.Add(contactview);


        }

        private void BTN_AfficherReseauxSociaux_Click(object sender, RoutedEventArgs e)
        {
            // Instancier la classe DAO_Reseaux avec le contexte approprié
            DAO_Reseaux daoReseaux = new DAO_Reseaux();

            // Récupérer les réseaux sociaux du contact sélectionné
            List<ContactReseauxSociaux> reseauxSociaux = daoReseaux.GetContactReseauxSociauxByContact(_selectedContact);

            // Vérifier et ajouter les réseaux sociaux manquants pour le contact sélectionné
            daoReseaux.AddMissingReseauxSociauxToContacts();

        
        }

   






    }
}


