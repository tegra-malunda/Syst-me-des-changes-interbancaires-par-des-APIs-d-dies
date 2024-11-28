using interbanque.DTO.client; 

namespace interbanque.DTO.interbanque
{
    public class  transfertReturnDTO
    {
        public string Id { get; set; } = null!;


        public compteReturnDTO FromCompte { get; set; } = null!;
        public string ToBanqueId { get; set; } = null!;
        public string ToBanqueNom { get; set; } = null!;

        public string ToNumeroCompte { get; set; } = null!;

        public decimal Montant { get; set; }
        public DateTime Date { get; set; }

    }
}
