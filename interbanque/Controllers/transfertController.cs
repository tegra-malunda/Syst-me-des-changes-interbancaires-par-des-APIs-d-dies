using Microsoft.AspNetCore.Mvc;
using interbanque.DTO;
using System.Data;
using interbanque.Models; 
using interbanque.Services;
using interbanque.DTO.interbanque;
using interbanque.DTO.retrait;
using interbanque.DTO.depot;

namespace interbanque.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class TransfertController : ControllerBase
    {
        private InterbanqueContext _bdContext;
        private readonly IConfiguration _conf;
        private ApiServiceExtern _ApiServiceExtern;
        public TransfertController(InterbanqueContext bdContext, IConfiguration conf, ApiServiceExtern ApiServiceExtern)
        {
            _bdContext = bdContext;
            _conf = conf;
            _ApiServiceExtern = ApiServiceExtern;
        }

        [HttpGet]
        [Route("liste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult<IEnumerable<transfertReturnDTO>> GetList( )
        {
           

            var liste =(from transfert in _bdContext.Transferts
                        join compte in _bdContext.Comptes on transfert.FromIdCompte equals compte.Id
                        join client in _bdContext.Clients on compte.IdClient equals client.Id
                        join banque in _bdContext.Banques on compte.IdBanque equals banque.Id
                        join To_banque in _bdContext.Banques on transfert.ToBanque equals To_banque.Id
                        select new transfertReturnDTO()  { 
                            Id = transfert.Id,
                            Montant = transfert.Montant,
                            ToBanqueId = transfert.ToBanque,
                            ToBanqueNom= To_banque.Nom,
                            ToNumeroCompte =transfert.ToNumeroCompte,
                            Date = transfert.Date,
                            FromCompte=new compteReturnDTO()
                            {
                                Id = compte.Id,
                                Devise =compte.Devise,
                                NomBanque=banque.Nom,
                                NumeroCompte=compte.NumeroCompte,
                                Client=new DTO.client.clientReturnDTO()
                                {
                                    Id = client.Id,
                                    Nom = client.Nom,
                                    Postnom = client.Postnom,
                                    Prenom = client.Prenom,
                                    Sexe = client.Sexe,
                                    Telephone = client.Telephone,
                                    Email = client.Email
                                }
                               
                            }
   }).OrderBy(Item => Item.FromCompte.Client.Nom).ThenBy(Item => Item.FromCompte.Client.Postnom).ThenBy(Item => Item.FromCompte.Client.Prenom).ToList();
            
             return Ok(liste); 
        }



        

        [HttpPost]
        [Route("ajouter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> ajouter([FromBody] transfertSaveDTO data)
        {
            try
            {
                var compte = _bdContext.Comptes.Where(Item => Item.Id == data.FromIdCompte).FirstOrDefault();
                 if (compte == null)
                { return Ok(new responseModel() { keyResponse = "compte_notfound", message = "Ce compte n'est pas trouvé " }); }

                var banqueSending = _bdContext.Banques.Where(Item => Item.Id ==compte.IdBanque).FirstOrDefault();
                if (banqueSending == null)
                { return Ok(new responseModel() { keyResponse = "banque_notfound", message = "Ce banque n'est pas trouvé " }); }

                var  BanqueReceiving = _bdContext.Banques.Where(Item => Item.Id == data.ToIdBanque).FirstOrDefault();


                var endpointretrait = Cryptage.decrypter(banqueSending.EndpointRetrait, _conf);
                var endpointdepot = Cryptage.decrypter(BanqueReceiving.EndpointDepot, _conf);
                var retrait =await _ApiServiceExtern.PostDataAsync<retraitViaInterbanqueSaveDTO>(endpointretrait,
                   new retraitViaInterbanqueSaveDTO() {
                   Code= banqueSending.Code,
                   Token = banqueSending.Token,
                   Devise=compte.Devise,
                   Montant=data.Montant,
                   numeroCompte= compte.NumeroCompte
                   });

               if(retrait.keyResponse!="reussi")
                { return BadRequest(retrait); }

                Guid guid = Guid.NewGuid();
                string IdGuid= guid.ToString();
                

                _bdContext.Transferts.Add(new  Transfert() {
                    Id= IdGuid,
                    FromIdCompte= data.FromIdCompte,
                    Montant= data.Montant,
                    ToBanque= data.ToIdBanque,
                    ToNumeroCompte= data.ToNumeroCompte
                    
                });
                var n = _bdContext.SaveChanges();

                var depot = await _ApiServiceExtern.PostDataAsync<depotViaInterbanqueSaveDTO>(endpointdepot,
                   new depotViaInterbanqueSaveDTO()
                   {
                       Code = BanqueReceiving.Code,
                       Token = BanqueReceiving.Token,
                       Devise = compte.Devise,
                       Montant = data.Montant,
                       numeroCompte = data.ToNumeroCompte
                   });

                

                if (n == 1)
                { return Ok(new responseModel() { message = "Tranfert réussi" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error "  }); }
        }

       


    }
}
