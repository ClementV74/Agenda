using System;
using System.Collections.Generic;

namespace calendrier2.contact_DB;

public partial class Tache
{
    public int Idtache { get; set; }

    public sbyte? Fait { get; set; }

    public TimeOnly? Temps { get; set; }

    public int TodolistIdtodolist { get; set; }

    public virtual Todolist TodolistIdtodolistNavigation { get; set; } = null!;
}
