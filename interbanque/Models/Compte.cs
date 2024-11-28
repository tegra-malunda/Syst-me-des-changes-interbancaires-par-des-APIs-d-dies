using System;
using System.Collections.Generic;

namespace interbanque.Models;

public partial class Compte
{
    public string Id { get; set; } = null!;

    public string IdClient { get; set; } = null!;

    public string IdBanque { get; set; } = null!;

    public string NumeroCompte { get; set; } = null!;

    public string Devise { get; set; } = null!;

    public virtual Banque IdBanqueNavigation { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual ICollection<Transfert> Transferts { get; set; } = new List<Transfert>();
}
