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
            return context.Taches.Include(t => t.TodolistIdtodolistNavigation).ToList(); // Récupération de toutes les tâches avec leur Todolist associée
        }
    }

    // Méthode pour ajouter un nouvel événement à la base de données
    public void AjouterEvenement(string titre, string description, TimeOnly? heure, string lieu) // Ajoutez les paramètres nécessaires
    {
        using (var context = new ContactContext())
        {
            // Recherche de la Todolist avec le nom spécifié
            Todolist todolist = context.Todolists.FirstOrDefault(t => t.Name == titre);

            // Si la Todolist avec ce nom n'existe pas, on la crée
            if (todolist == null)
            {
                todolist = new Todolist { Name = titre }; // Création d'un nouvel objet Todolist
                context.Todolists.Add(todolist); // Ajout de la Todolist à la base de données
                context.SaveChanges(); // Enregistrement des modifications
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

    public void ModifierEvenement(int id, string titre, string description, TimeOnly? heure, string lieu) // Ajoutez les paramètres nécessaires
    {
        using (var context = new ContactContext())
        {
            // Recherche de la tâche avec l'ID spécifié
            Tache evenement = context.Taches.FirstOrDefault(t => t.Idtache == id);

            if (evenement != null) // Si la tâche existe
            {
                // Recherche de la Todolist avec le nom spécifié
                Todolist todolist = context.Todolists.FirstOrDefault(t => t.Name == titre);

                // Si la Todolist avec ce nom n'existe pas, on la crée
                if (todolist == null)
                {
                    todolist = new Todolist { Name = titre }; // Création d'un nouvel objet Todolist
                    context.Todolists.Add(todolist); // Ajout de la Todolist à la base de données
                    context.SaveChanges(); // Enregistrement des modifications
                }

                // Mise à jour des propriétés de la tâche
                evenement.Description = description;
                evenement.Temps = heure;
                evenement.Lieux = lieu;
                evenement.TodolistIdtodolist = todolist.Idtodolist;

                // Enregistrement des modifications
                context.SaveChanges();
            }
        }
    }

    public void SupprimerEvenement(int id) // Ajoutez le paramètre nécessaire
    {
        using (var context = new ContactContext())
        {
            // Recherche de la tâche avec l'ID spécifié
            Tache evenement = context.Taches.FirstOrDefault(t => t.Idtache == id);

            if (evenement != null) // Si la tâche existe
            {
                context.Taches.Remove(evenement); // Suppression de la tâche du contexte
                context.SaveChanges(); // Enregistrement des modifications
            }
        }
    }



  



}
