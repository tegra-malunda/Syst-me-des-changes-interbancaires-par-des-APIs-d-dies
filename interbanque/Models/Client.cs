using System;
using System.Collections.Generic;

namespace interbanque.Models;

public partial class Client
{
    public string Id { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Postnom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Motpasse { get; set; } = null!;

    public string Sexe { get; set; } = null!;

    public virtual ICollection<Compte> Comptes { get; set; } = new List<Compte>();
}
