using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.interbanque
{
    public class  banqueReturnDTO
    {
        public string Id { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Nom { get; set; } = null!; 
    }
}
