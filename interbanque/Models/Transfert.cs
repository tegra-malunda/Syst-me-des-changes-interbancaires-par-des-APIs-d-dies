using System;
using System.Collections.Generic;

namespace interbanque.Models;

public partial class Transfert
{
    public string Id { get; set; } = null!;

    public string FromIdCompte { get; set; } = null!;

    public string ToBanque { get; set; } = null!;

    public string ToNumeroCompte { get; set; } = null!;

    public decimal Montant { get; set; }

    public DateTime Date { get; set; }

    public virtual Compte FromIdCompteNavigation { get; set; } = null!;

    public virtual Banque ToBanqueNavigation { get; set; } = null!;
}
