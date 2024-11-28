using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.compte
{
    public class compteSoldeDTO
    {
         
        [Required]
        public string numero_compte { get; set; } = null!;
    }
}
