﻿using calendrier2.contact_DB;
using calendrier2.service.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace calendrier2.view
{
    public partial class view_contact : UserControl
    {
        private DAO_contact _daoContact;
        private DAO_Reseaux daoReseaux;
        private List<string> _statusList;
      
        private ContactContext _dbContext;

        // Propriété pour la liste des contacts
        public ObservableCollection<Contact> Contacts { get; set; }



        public view_contact()
        {
            InitializeComponent();




            // Lier le DataContext de votre UserControl à lui-même pour utiliser le Binding
            this.DataContext = this;

            _daoContact = new DAO_contact(); // Initialisez votre objet DAO_contact
            _dbContext = new ContactContext();
            daoReseaux = new DAO_Reseaux();
            Contacts = new ObservableCollection<Contact>(); // Initialisez la liste observable des contacts
            LoadStatusList(); // Chargez la liste des statuts
            LoadContacts(); // Chargez les contacts depuis la base de données

  
        }


        private void LoadStatusList()
        {
       


            try
            {
                _statusList = _daoContact.GetStatusList(); // Appel de la méthode dans votre DAO pour récupérer la liste des statuts
                StatusFilterListBox.ItemsSource = _statusList; // Liaison de la ListBox avec la liste des statuts
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des statuts : " + ex.Message, "Erreur de Chargement", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

      
        private void LoadContacts()
        {
            try
            {
                // Récupérer la liste des contacts depuis la base de données
                IEnumerable<Contact> contactsFromDatabase = _daoContact.GetContacts();

                // Créer une nouvelle ObservableCollection<Contact> à partir des contacts récupérés
                Contacts = new ObservableCollection<Contact>(contactsFromDatabase);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des contacts : " + ex.Message, "Erreur de Chargement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void StatusFilterListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Effacez les contacts actuellement affichés
            Contacts.Clear();

            // Vérifiez si l'option "Tous" est sélectionnée
            if (StatusFilterListBox.SelectedItems.Contains("Tous"))
            {
                // Si "Tous" est sélectionné, affichez tous les contacts
                foreach (var contact in _dbContext.Contacts)
                {
                    Contacts.Add(contact);
                }
            }
            else
            {
                // Parcourez toutes les catégories sélectionnées
                foreach (string selectedStatus in StatusFilterListBox.SelectedItems)
                {
                    // Filtrer les contacts en fonction du statut sélectionné
                    List<Contact> filteredContacts = _dbContext.Contacts.Where(c => c.Status == selectedStatus).ToList();

                    // Ajouter les contacts filtrés à la liste des contacts
                    foreach (var contact in filteredContacts)
                    {
                        Contacts.Add(contact);
                    }
                }
            }
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
            var addcontactview = new view_addcontact();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(addcontactview, 2);
            Ecran_Contact.Children.Add(addcontactview);
        }

        private void BTN_Supp_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridContacts.SelectedItem is Contact contactASupprimer)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce contact ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Supprimer tous les réseaux sociaux liés à ce contact
                  
                    daoReseaux.RemoveReseauxSociauxFromContact(contactASupprimer);

                    // Supprimer le contact lui-même
                    _daoContact.DeleteContact(contactASupprimer.IdContact);
                    Contacts.Remove(contactASupprimer);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un contact à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void BTN_Modifier_CLick(object sender, RoutedEventArgs e)
        {
            if (DataGridContacts.SelectedItem is Contact contactAModifier)
            {
                // Récupérer le contact à modifier depuis la base de données
                Contact contactModifie = Contacts.FirstOrDefault(c => c.IdContact == contactAModifier.IdContact);

                if (contactModifie != null)
                {
                    // Mettez à jour les propriétés du contact avec les valeurs de la ligne sélectionnée
                    contactModifie.Nom = contactAModifier.Nom;
                    contactModifie.Prenom = contactAModifier.Prenom;
                    contactModifie.Email = contactAModifier.Email;
                    contactModifie.Tel = contactAModifier.Tel;

                    // Appeler la méthode UpdateContact pour enregistrer les modifications dans la base de données
                    _daoContact.UpdateContact(contactModifie);

                    // Rafraîchir la liste des contacts après la mise à jour
                    RefreshContacts();
                    MessageBox.Show("Le contact a été mis à jour avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Le contact sélectionné n'existe pas dans la base de données.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        // Méthode pour rafraîchir la liste des contacts
        private void RefreshContacts()
        {
            
            foreach (var contact in Contacts.ToList())
            {
                Contacts.Add(contact);
            }
        }


        private void TB_Search(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower(); // Convertissez le texte en minuscules pour une comparaison insensible à la casse
            ICollectionView cv = CollectionViewSource.GetDefaultView(DataGridContacts.ItemsSource);
            if (string.IsNullOrEmpty(searchText))
            {
                cv.Filter = null;
            }
            else
            {
                cv.Filter = o =>
                {
                    // Vérifiez si le texte de recherche correspond à l'un des champs dans la ligne de la DataGrid
                    var contact = o as Contact;
                    return contact.Nom.ToLower().Contains(searchText) ||
                           contact.Prenom.ToLower().Contains(searchText) ||
                           contact.Email.ToLower().Contains(searchText) ||
                           contact.Tel.ToLower().Contains(searchText);

                };
            }
        }

        private void BTN_Details_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridContacts.SelectedItem is Contact selectedContact)
            {
                var detailview = new view_DetailUser(selectedContact);
                Ecran_Contact.Children.Clear();
                Grid.SetColumnSpan(detailview, 2);
                Ecran_Contact.Children.Add(detailview);

            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un contact pour afficher les détails.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }





        }
    }
}
