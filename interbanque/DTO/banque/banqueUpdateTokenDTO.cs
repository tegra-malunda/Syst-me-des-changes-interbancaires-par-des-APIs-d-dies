using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.interbanque
{
    public class banqueUpdateTokenDTO
    {
        [Required]
        public string id { get; set; } = null!;
        [Required]
        public string Token_old { get; set; } = null!;
        [Required]
        public string Token_new { get; set; } = null!;
        
    }
}
