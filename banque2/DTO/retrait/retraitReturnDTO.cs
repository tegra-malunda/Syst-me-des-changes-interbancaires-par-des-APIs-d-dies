using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.retrait
{
    public class retraitReturnDTO
    {
        public string Id { get; set; } = null!;

        public string IdCompte { get; set; } = null!;

        public string numeroCompte { get; set; } = null!;
        public string Devise { get; set; } = null!;

        public string? FromInterbanque { get; set; }

        public decimal Montant { get; set; }

        public DateTime Date { get; set; }
    }
}
