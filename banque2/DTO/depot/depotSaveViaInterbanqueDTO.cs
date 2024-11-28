using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.depot
{
    public class depotViaInterbanqueSaveDTO
    {
        public string? Code { get; set; } = null!;

        public string? Token { get; set; } = null!;

        public string numeroCompte { get; set; } = null!;
        public string Devise {  get; set; } = null!;
        public decimal Montant { get; set; } 
    }
}
