using System;
using System.Collections.Generic;

namespace calendrier2.contact_DB;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
