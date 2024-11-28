using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.depot
{
    public class depotReturnDTO
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
