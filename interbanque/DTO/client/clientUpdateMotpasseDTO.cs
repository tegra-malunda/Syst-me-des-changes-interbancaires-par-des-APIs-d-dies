using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.compte
{
    public class clientUpdateMotpasseDTO
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string motpasse_old { get; set; } = null!;
        [motpasseValidation]
        public string motpasse_new { get; set; } = null!;
        
    }
}
