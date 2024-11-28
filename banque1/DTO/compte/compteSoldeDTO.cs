using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.compte
{
    public class compteSoldeDTO
    {
         
        [Required]
        public string numero_compte { get; set; } = null!;
    }
}
