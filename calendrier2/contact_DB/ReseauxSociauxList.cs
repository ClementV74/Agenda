using System;
using System.Collections.Generic;

namespace calendrier2.contact_DB;

public partial class ReseauxSociauxList
{
    public int IdReseau { get; set; }

    public string? NomReseau { get; set; }

    public virtual ICollection<ContactReseauxSociaux> ContactReseauxSociauxes { get; set; } = new List<ContactReseauxSociaux>();
}
