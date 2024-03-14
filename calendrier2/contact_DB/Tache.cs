using System;
using System.Collections.Generic;
using calendrier2.contact_DB;

namespace calendrier2.contact_DB;

public partial class Tache
{
    public int Idtache { get; set; }

    public bool? Fait { get; set; }

    public TimeOnly? Temps { get; set; }

    public int TodolistIdtodolist { get; set; }

    public virtual Todolist TodolistIdtodolistNavigation { get; set; } = null!;
}
