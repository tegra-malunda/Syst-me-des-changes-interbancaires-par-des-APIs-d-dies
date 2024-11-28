using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.retrait
{
    public class retraitSaveDTO
    { 
        public string numeroCompte { get; set; } = null!; 

        public decimal Montant { get; set; } 
    }
}
