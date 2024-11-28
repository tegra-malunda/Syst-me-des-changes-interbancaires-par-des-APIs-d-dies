using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.interbanque
{
    public class banqueUpdateEnpointRetraitDTO
    {
        [Required]
        public string id { get; set; } = null!;
        [Required]
        public string EnpointRetrait { get; set; } = null!;

    }
}
