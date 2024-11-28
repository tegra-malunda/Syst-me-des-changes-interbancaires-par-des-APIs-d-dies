using System;
using System.Collections.Generic;

namespace interbanque.Models;

public partial class Banque
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string EndpointDepot { get; set; } = null!;

    public string EndpointRetrait { get; set; } = null!;

    public virtual ICollection<Compte> Comptes { get; set; } = new List<Compte>();

    public virtual ICollection<Transfert> Transferts { get; set; } = new List<Transfert>();
}
