using System;
using System.Collections.Generic;

namespace calendrier2.contact_DB;

public partial class Todolist
{
    public int Idtodolist { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Tache> Taches { get; set; } = new List<Tache>();
}
