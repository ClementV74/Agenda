using System;
using System.Collections.Generic;

namespace calendrier2.contact_DB;

public partial class Contact
{
    public int IdContact { get; set; }

    public string? Prenom { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<ContactReseauxSociaux> ContactReseauxSociauxes { get; set; } = new List<ContactReseauxSociaux>();
}
