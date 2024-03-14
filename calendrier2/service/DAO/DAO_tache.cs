using calendrier2.contact_DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class DAO_tache
{
    public List<Tache> GetRappels()
    {
        using (var context = new ContactContext())
        {
            return context.Taches.Include(t => t.TodolistIdtodolistNavigation).ToList();
        }
    }


    public void AjouterTache(Tache nouvelleTache)
    {
        using (var context = new ContactContext())
        {
            context.Taches.Add(nouvelleTache);
            context.SaveChanges();
        }
    }
}
