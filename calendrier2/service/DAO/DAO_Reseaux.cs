﻿using System.Collections.Generic;
using System.Linq;
using calendrier2.contact_DB;
using Microsoft.EntityFrameworkCore; // Importez ce namespace

namespace calendrier2.service.DAO
{
    public class DAO_Reseaux
    {
        private readonly ContactContext _context;

        public DAO_Reseaux(ContactContext context)
        {
            _context = context;
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
    }
}