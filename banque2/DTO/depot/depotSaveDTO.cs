using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.depot
{
    public class depotSaveDTO
    { 
        public string numeroCompte { get; set; } = null!; 

        public decimal Montant { get; set; } 
    }
}
