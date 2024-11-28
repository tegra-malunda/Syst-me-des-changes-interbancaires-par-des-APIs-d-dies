using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.interbanque
{
    public class  banqueUpdateCodeDTO
    {
        [Required]
        public string id { get; set; } = null!;
        [Required]
        public string Code_old { get; set; } = null!;
        [Required]
        public string Code_new { get; set; } = null!;
        
    }
}
