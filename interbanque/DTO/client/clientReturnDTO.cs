 
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.client
{
    public class clientReturnDTO
    {
        public string Id { get; set; } = null!;

        public string Nom { get; set; } = null!;

        public string Postnom { get; set; } = null!;

        public string Prenom { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telephone { get; set; } = null!; 

        public string Sexe { get; set; } = null!;
    }
}
