using System;
using System.Collections.Generic;

namespace banque2.Models;

public partial class Compte
{
    public string Id { get; set; } = null!;

    public string NumeroCompte { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Postnom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Adresse { get; set; } = null!;

    public string Sexe { get; set; } = null!;

    public string Devise { get; set; } = null!;

    public string CodePin { get; set; } = null!;

    public virtual ICollection<Depot> Depots { get; set; } = new List<Depot>();

    public virtual ICollection<Retrait> Retraits { get; set; } = new List<Retrait>();
}
