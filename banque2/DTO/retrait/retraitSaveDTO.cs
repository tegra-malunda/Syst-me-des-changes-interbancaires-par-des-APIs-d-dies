using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.retrait
{
    public class retraitSaveDTO
    { 
        public string numeroCompte { get; set; } = null!; 

        public decimal Montant { get; set; } 
    }
}
