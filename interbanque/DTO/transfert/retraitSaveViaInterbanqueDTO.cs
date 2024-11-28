 
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.retrait
{
    public class retraitViaInterbanqueSaveDTO
    {
        public string? Code { get; set; } = null!;

        public string? Token { get; set; } = null!;

        public string numeroCompte { get; set; } = null!;
        public string Devise {  get; set; } = null!;
        public decimal Montant { get; set; } 
    }
}
