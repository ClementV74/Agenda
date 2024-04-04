using System.Collections.Generic;
using System.Linq;
using calendrier2.contact_DB;
using Microsoft.EntityFrameworkCore; // Importez ce namespace

namespace calendrier2.service.DAO
{
    public class DAO_Reseaux
    {
        private readonly ContactContext _context;

        public DAO_Reseaux()
        {
            _context = new ContactContext();
        }

        public List<ContactReseauxSociaux> GetContactReseauxSociauxByContact(Contact contact)
        {
            // Recherche des réseaux sociaux pour le contact spécifié
            var reseauxSociaux = _context.ContactReseauxSociauxes
                .Include(crs => crs.IdReseauNavigation) // Inclure les informations du réseau social
                .Where(crs => crs.IdContact == contact.IdContact) // Filtre sur le contact spécifié
                .ToList(); // Récupérer les résultats

            return reseauxSociaux;
        }

        public void AddReseauSocialToContact(Contact contact, ReseauxSociauxList reseau)
        {
            // Créer une nouvelle entrée pour le contact et le réseau social
            var newReseauSociaux = new ContactReseauxSociaux
            {
                IdContact = contact.IdContact,
                IdReseau = reseau.IdReseau
            };

            // Ajouter la nouvelle entrée à la base de données
            _context.ContactReseauxSociauxes.Add(newReseauSociaux);
            _context.SaveChanges();
        }


        public void SaveChangesAndUpdateReseaux(List<ContactReseauxSociaux> reseauxSociaux)
        {
            using (var context = new ContactContext())
            {
                foreach (var reseau in reseauxSociaux)
                {
                    // Vérifier si l'objet est déjà attaché au contexte
                    if (context.Entry(reseau).State == EntityState.Detached)
                        context.Set<ContactReseauxSociaux>().Attach(reseau);

                    // Marquer l'objet comme modifié pour qu'il soit enregistré dans la base de données
                    context.Entry(reseau).State = EntityState.Modified;
                }

                // Enregistrer les modifications dans la base de données
                context.SaveChanges();
            }
        }

        public void RemoveReseauSocialFromContact(Contact contact, ReseauxSociauxList reseau)
        {
            // Recherche de l'entrée pour le contact et le réseau social
            var reseauSociaux = _context.ContactReseauxSociauxes
                .Where(crs => crs.IdContact == contact.IdContact && crs.IdReseau == reseau.IdReseau)
                .FirstOrDefault();

            // Supprimer l'entrée de la base de données
            _context.ContactReseauxSociauxes.Remove(reseauSociaux);
            _context.SaveChanges();
        }

        public List<ReseauxSociauxList> GetReseauxSociauxList()
        {
            // Récupérer la liste des réseaux sociaux
            return _context.ReseauxSociauxLists.ToList();
        }

        public void AddMissingReseauxSociauxToContacts()
        {
            // Récupérer tous les contacts de la base de données
            var allContacts = _context.Contacts.ToList();

            // Récupérer la liste des réseaux sociaux disponibles
            var reseauxSociauxList = GetReseauxSociauxList();

            // Parcourir tous les contacts
            foreach (var contact in allContacts)
            {
                // Récupérer les réseaux sociaux associés à ce contact
                var contactReseauxSociaux = GetContactReseauxSociauxByContact(contact);

                // Récupérer les identifiants des réseaux sociaux associés à ce contact
                var contactReseauxSociauxIds = contactReseauxSociaux.Select(crs => crs.IdReseau).ToList();

                // Trouver les identifiants des réseaux sociaux manquants
                var missingReseauxSociauxIds = reseauxSociauxList.Select(r => r.IdReseau).Except(contactReseauxSociauxIds).ToList();

                // Si des réseaux sociaux sont manquants, les ajouter au contact
                foreach (var reseauId in missingReseauxSociauxIds)
                {
                    var reseau = reseauxSociauxList.FirstOrDefault(r => r.IdReseau == reseauId);
                    if (reseau != null)
                    {
                        AddReseauSocialToContact(contact, reseau);
                    }
                }
            }
        }

        public void RemoveReseauxSociauxFromContact(Contact contact)
        {
            // Recherche des réseaux sociaux liés au contact
            var reseauxSociaux = _context.ContactReseauxSociauxes
                .Where(crs => crs.IdContact == contact.IdContact)
                .ToList();

            // Suppression des réseaux sociaux associés au contact
            _context.ContactReseauxSociauxes.RemoveRange(reseauxSociaux);
            _context.SaveChanges();
        }



    }
}
