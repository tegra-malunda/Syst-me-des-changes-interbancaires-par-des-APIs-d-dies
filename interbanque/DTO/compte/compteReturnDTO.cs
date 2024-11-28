using interbanque.DTO.client; 

namespace interbanque.DTO.interbanque
{
    public class  compteReturnDTO
    {
        public string Id { get; set; } = null!;

        public clientReturnDTO Client { get; set; } = null!;

        public string NomBanque { get; set; } = null!;
        public string NumeroCompte { get; set; } = null!;

        public string Devise { get; set; } = null!;

    }
}
