using calendrier2.contact_DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace calendrier2.service.DAO
{
    public class DAO_contact
    {
        private ContactContext _dbContext = new ContactContext(); // Contexte de la base de données


        // Méthode pour ajouter un nouveau contact
        public void AddContact(Contact newContact)
        {
            try
            {
                _dbContext.Contacts.Add(newContact); // Ajouter le nouveau contact au contexte
                _dbContext.SaveChanges(); // Enregistrer les modifications dans la base de données
            }
            catch (Exception ex)
            {
                // Gérer les exceptions
                Console.WriteLine("Erreur lors de l'ajout du contact : " + ex.Message);
                // Vous pouvez choisir de lever une exception, d'enregistrer l'erreur dans un journal, ou d'effectuer toute autre action appropriée.
            }
        }

        // Méthode pour récupérer la liste de tous les contacts
        public IEnumerable<Contact> GetContacts()
        {
            return _dbContext.Contacts.ToList(); // Récupérer tous les contacts de la base de données
        }


        // Méthode pour mettre à jour un contact existant
        public void UpdateContact(Contact updatedContact) // Prend en paramètre un objet Contact mis à jour
        {
            var existingContact = _dbContext.Contacts.FirstOrDefault(c => c.IdContact == updatedContact.IdContact); // Rechercher le contact existant dans la base de données
            if (existingContact != null) // Si le contact existe
            {
                // Mettre à jour les propriétés du contact existant
                existingContact.Prenom = updatedContact.Prenom; // Mettre à jour le prénom
                existingContact.Nom = updatedContact.Nom; // Mettre à jour le nom
                existingContact.Email = updatedContact.Email; // Mettre à jour l'email
                existingContact.Tel = updatedContact.Tel; // Mettre à jour le numéro de téléphone

                _dbContext.SaveChanges(); // Enregistrer les modifications dans la base de données
            }
            else
            {
                // Le contact à mettre à jour n'existe pas dans la base de données
                // Vous pouvez choisir de lever une exception, de créer un nouveau contact, ou d'effectuer toute autre action appropriée.
            }
        }

        // Méthode pour supprimer un contact
        public void DeleteContact(int contactId) // Prend en paramètre l'ID du contact à supprimer
        { 
            var contactToDelete = _dbContext.Contacts.FirstOrDefault(c => c.IdContact == contactId); // Rechercher le contact à supprimer dans la base de données
            if (contactToDelete != null) // Si le contact existe
            {
                _dbContext.Contacts.Remove(contactToDelete); // Supprimer le contact du contexte
               
                
                _dbContext.SaveChanges(); // Enregistrer les modifications dans la base de données
            }
            else
            {
                // Le contact à supprimer n'existe pas dans la base de données
                // Vous pouvez choisir de lever une exception ou d'effectuer toute autre action appropriée.
            }
        }
        public List<string> GetStatusList() 
        {
            List<string> statusList = new List<string>();

            // Ajoutez tous les statuts possibles à la liste
            statusList.Add("Tous");
            statusList.Add("Famille");
            statusList.Add("Travail");
            statusList.Add("Ami");
           

            return statusList; // Retourne la liste des statuts
        }

        public bool IsDatabase_existe()
        {
            try
            {
                _dbContext.Database.GetConnectionString(); // Vérifier la chaîne de connexion
                _dbContext.Database.CanConnect(); // Vérifier la connexion à la base de données
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la vérification de la connexion à la base de données : " + ex.Message);
                return false;
            }
        }


    }
}
