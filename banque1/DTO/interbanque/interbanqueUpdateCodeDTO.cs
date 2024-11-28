using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.interbanque
{
    public class InterbanqueUpdateCodeDTO
    {
        [Required]
        public string id { get; set; } = null!;
        [Required]
        public string Code_old { get; set; } = null!;
        [Required]
        public string Code_new { get; set; } = null!;
        
    }
}
