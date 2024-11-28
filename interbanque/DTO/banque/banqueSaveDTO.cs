using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.interbanque
{
    public class  banqueSaveDTO
    {
        
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public string Nom { get; set; } = null!;
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        public string EndpointDepot { get; set; } = null!;
        [Required]
        public string EndpointRetrait { get; set; } = null!;
    }
}
