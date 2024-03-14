using calendrier2.contact_DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace calendrier2.service.DAO
{
    public class DAO_contact
    {
        private ContactContext _dbContext = new ContactContext();


        // Méthode pour ajouter un nouveau contact
        public void AddContact(Contact newContact)
        {
            try
            {
                _dbContext.Contacts.Add(newContact);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Gérer les exceptions
                Console.WriteLine("Erreur lors de l'ajout du contact : " + ex.Message);
                // Vous pouvez choisir de lever une exception, d'enregistrer l'erreur dans un journal, ou d'effectuer toute autre action appropriée.
            }
        }

        // Méthode pour mettre à jour un contact existant
        public void UpdateContact(Contact updatedContact)
        {
            var existingContact = _dbContext.Contacts.FirstOrDefault(c => c.IdContact == updatedContact.IdContact);
            if (existingContact != null)
            {
                // Mettre à jour les propriétés du contact existant
                existingContact.Prenom = updatedContact.Prenom;
                existingContact.Nom = updatedContact.Nom;
                existingContact.Email = updatedContact.Email;
                existingContact.Tel = updatedContact.Tel;

                _dbContext.SaveChanges();
            }
            else
            {
                // Le contact à mettre à jour n'existe pas dans la base de données
                // Vous pouvez choisir de lever une exception, de créer un nouveau contact, ou d'effectuer toute autre action appropriée.
            }
        }

        // Méthode pour supprimer un contact
        public void DeleteContact(int contactId)
        {
            var contactToDelete = _dbContext.Contacts.FirstOrDefault(c => c.IdContact == contactId);
            if (contactToDelete != null)
            {
                _dbContext.Contacts.Remove(contactToDelete);
                _dbContext.SaveChanges();
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
           

            return statusList;
        }

    }
}
