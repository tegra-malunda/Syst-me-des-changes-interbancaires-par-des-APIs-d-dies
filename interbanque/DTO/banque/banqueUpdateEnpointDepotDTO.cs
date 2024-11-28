using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.interbanque
{
    public class banqueUpdateEnpointDepotDTO
    {
        [Required]
        public string id { get; set; } = null!;
        [Required]
        public string EnpointDepot { get; set; } = null!; 
        
    }
}
