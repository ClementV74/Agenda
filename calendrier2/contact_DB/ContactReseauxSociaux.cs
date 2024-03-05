using System;
using System.Collections.Generic;

namespace calendrier2.contact_DB;

public partial class ContactReseauxSociaux
{
    public int IdContact { get; set; }

    public int IdReseau { get; set; }

    public string? Pseudo { get; set; }

    public string? Url { get; set; }

    public virtual Contact IdContactNavigation { get; set; } = null!;

    public virtual ReseauxSociauxList IdReseauNavigation { get; set; } = null!;
}
