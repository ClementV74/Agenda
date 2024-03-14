using calendrier2.contact_DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class DAO_tache
{
    // Méthode pour récupérer les rappels de la base de données
    public List<Tache> GetRappels()
    {
        using (var context = new ContactContext())
        {
            return context.Taches.Include(t => t.TodolistIdtodolistNavigation).ToList();
        }
    }

    // Méthode pour ajouter un nouvel événement à la base de données
    public void AjouterEvenement(string titre, string description, TimeOnly? heure, string lieu)
    {
        using (var context = new ContactContext())
        {
            // Recherche de la Todolist avec le nom spécifié
            Todolist todolist = context.Todolists.FirstOrDefault(t => t.Name == titre);

            // Si la Todolist avec ce nom n'existe pas, on la crée
            if (todolist == null)
            {
                todolist = new Todolist { Name = titre };
                context.Todolists.Add(todolist);
                context.SaveChanges();
            }

            // Création d'un nouvel objet Tache avec les informations fournies
            Tache nouvelEvenement = new Tache
            {
                Description = description,
                Temps = heure,
                Lieux = lieu,
                TodolistIdtodolist = todolist.Idtodolist
            };

            // Ajout du nouvel événement à la base de données
            context.Taches.Add(nouvelEvenement);
            context.SaveChanges();
        }
    }

  



}
