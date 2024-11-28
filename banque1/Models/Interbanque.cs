using System;
using System.Collections.Generic;

namespace banque1.Models;

public partial class Interbanque
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Token { get; set; } = null!;

    public virtual ICollection<Depot> Depots { get; set; } = new List<Depot>();

    public virtual ICollection<Retrait> Retraits { get; set; } = new List<Retrait>();
}
