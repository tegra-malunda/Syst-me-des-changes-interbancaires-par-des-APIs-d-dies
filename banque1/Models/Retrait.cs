using System;
using System.Collections.Generic;

namespace banque1.Models;

public partial class Retrait
{
    public string Id { get; set; } = null!;

    public string IdCompte { get; set; } = null!;

    public string? FromInterbanque { get; set; }

    public decimal Montant { get; set; }

    public DateTime Date { get; set; }

    public virtual Interbanque? FromInterbanqueNavigation { get; set; }

    public virtual Compte IdCompteNavigation { get; set; } = null!;
}
