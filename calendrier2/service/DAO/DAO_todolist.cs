using calendrier2.contact_DB;
using System.Collections.Generic;
using System.Linq;

public class DAO_todolist
{
    public List<Tache> GetTaches(int idTodolist) // Ajoutez le paramètre nécessaire
    {
        using (var context = new ContactContext()) 
        {
            return context.Taches.Where(t => t.TodolistIdtodolist == idTodolist).ToList(); // Récupération de toutes les tâches associées à la Todolist spécifiée
        }
    }
}
