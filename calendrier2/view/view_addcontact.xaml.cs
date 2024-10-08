﻿using calendrier2.contact_DB;
using calendrier2.service.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace calendrier2.view
{
    /// <summary>
    /// Logique d'interaction pour view_addcontact.xaml
    /// </summary>
    public partial class view_addcontact : UserControl
    {
        private ContactContext _dbContext = new ContactContext();
        private DAO_contact _daoContact = new DAO_contact(); // instance objet DAO_contact

        // Propriété pour la liste des contacts
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();
        public view_addcontact()
        {
            InitializeComponent();
            // Lier le DataContext de votre UserControl à lui-même pour utiliser le Binding
            this.DataContext = this;

            // Charger les contacts depuis la base de données
            Contacts = new ObservableCollection<Contact>(_dbContext.Contacts.ToList());
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

        private void BTN_AddContact_Click(object sender, RoutedEventArgs e)
        {
            // Créer un nouveau contact en fonction des valeurs des champs
            Contact newContact = new Contact
            {
                Prenom = PrenomTextBox.Text,
                Nom = NomTextBox.Text,
                Email = EmailTextBox.Text,
                Tel = TelTextBox.Text
            };

            // Ajouter les catégories sélectionnées au contact
            if (FamilleCheckBox.IsChecked == true)
            {
                newContact.Status += "Famille, ";
            }
            if (AmiCheckBox.IsChecked == true)
            {
                newContact.Status += "Ami, ";
            }
            if (TravailCheckBox.IsChecked == true)
            {
                newContact.Status += "Travail, ";
            }

            // Supprimer la virgule supplémentaire à la fin de la chaîne de statut
            if (!string.IsNullOrEmpty(newContact.Status))
            {
                newContact.Status = newContact.Status.Remove(newContact.Status.Length - 2);
            }

            // Ajouter le contact en utilisant le DAO_contact
            DAO_contact dao = new DAO_contact();
            dao.AddContact(newContact);

            // Récupérer la liste des réseaux sociaux disponibles
            DAO_Reseaux daoReseaux = new DAO_Reseaux();
            var reseauxSociauxList = daoReseaux.GetReseauxSociauxList();

            // Ajouter tous les réseaux sociaux disponibles au nouveau contact
            foreach (var reseau in reseauxSociauxList)
            {
                daoReseaux.AddReseauSocialToContact(newContact, reseau);
            }

            var contactview = new view_contact(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord

            // Remplace le contenu des deux parties de la grille
            Ecran_Contact.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(contactview, 2);
            Ecran_Contact.Children.Add(contactview);
        }


        private void BTN_retour_Click(object sender, RoutedEventArgs e)
        {
            var contactview = new view_contact();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(contactview, 2);
            Ecran_Contact.Children.Add(contactview);

        }
    }


}
