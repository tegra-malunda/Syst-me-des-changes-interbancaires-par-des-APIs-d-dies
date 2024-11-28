using Microsoft.AspNetCore.Mvc;
using interbanque.DTO;
using System.Data;
using interbanque.Models; 
using interbanque.Services;
using interbanque.DTO.interbanque;

namespace interbanque.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class CompteController : ControllerBase
    {
        private InterbanqueContext _bdContext;
        private readonly IConfiguration _conf;
        public CompteController(InterbanqueContext bdContext, IConfiguration conf)
        {
            _bdContext = bdContext;
            _conf = conf;
        }

        [HttpGet]
        [Route("liste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult<IEnumerable<compteReturnDTO>> GetList( )
        {
           

            var liste =(from compte in _bdContext.Comptes
                        join client in _bdContext.Clients on compte.IdClient equals client.Id
                        join banque in _bdContext.Banques on compte.IdBanque equals banque.Id
                        select new  compteReturnDTO()  {
                          Id = compte.Id,
                          Devise=compte.Devise,
                          NumeroCompte=compte.NumeroCompte,
                          NomBanque=banque.Nom,
                          Client=new DTO.client.clientReturnDTO()
                          {
                              Id = client.Id,
                              Nom=client.Nom,
                              Postnom=client.Postnom,
                              Prenom=client.Prenom,
                              Sexe=client.Sexe,
                              Telephone=client.Telephone,
                              Email=client.Email
                              
                          }
                          
                
                
            }).OrderBy(Item => Item.Client.Nom).ThenBy(Item => Item.Client.Postnom).ThenBy(Item => Item.Client.Prenom).ToList();
            
             return Ok(liste); 
        }



        

        [HttpPost]
        [Route("ajouter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult ajouter([FromBody] compteSaveDTO data)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string IdGuid= guid.ToString();
                

                _bdContext.Comptes.Add(new  Compte() {
                    Id= IdGuid,
                    Devise = data.Devise,
                    IdClient=data.IdClient,
                    IdBanque=data.IdBanque,
                    NumeroCompte=data.NumeroCompte 
                });
                var n = _bdContext.SaveChanges();
                
                if (n == 1)
                { return Ok(new responseModel() { message = "Enregistrement réussi" }); }
                else { return BadRequest(new responseModel() { message = "Enregistrement échoué" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error "  }); }
        }

       


    }
}
