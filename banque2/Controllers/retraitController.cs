using Microsoft.AspNetCore.Mvc;
 
using System.Data;
using banque2.Models;
using banque2.DTO.retrait;
using banque2.DTO;
using System.Net;

namespace banque2.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class retraitController : ControllerBase
    {
        private Banque2Context _bdContext;
        private readonly IConfiguration _conf;
        public retraitController(Banque2Context bdContext, IConfiguration conf)
        {
            _bdContext = bdContext;
            _conf = conf;
        }

        [HttpGet]
        [Route("liste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult<IEnumerable<retraitReturnDTO>> GetList( )
        {
           

            var liste =(from retrait in _bdContext.Retraits
                       join compte in _bdContext.Comptes on retrait.IdCompte equals compte.Id
                 select new retraitReturnDTO()  { Id = retrait.Id, 
                 FromInterbanque=retrait.FromInterbanque,
                 IdCompte=retrait.IdCompte,
                 numeroCompte=compte.NumeroCompte,
                 Montant = retrait.Montant,
                     Devise = compte.Devise,
                     Date = retrait.Date
            }).OrderBy(Item => Item.Date).ToList();
            
             return Ok(liste); 
        }



        

        [HttpPost]
        [Route("ajouter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult ajouter([FromBody] retraitSaveDTO data)
        {

            try
            {
                var compte = _bdContext.Comptes.Where(item => item.NumeroCompte == data.numeroCompte).FirstOrDefault();
                 if (compte == null)
                { return Ok(new responseModel() { keyResponse = "compte_notfound", message = "Ce numéro de compte n'existe pas" }); }

                var sommeDepots = _bdContext.Depots.Where(item => item.IdCompte == compte.Id).Sum(Item => Item.Montant);
                var sommeRetraits = _bdContext.Retraits.Where(item => item.IdCompte == compte.Id).Sum(Item => Item.Montant);

                var solde = sommeDepots - sommeRetraits;
                if (solde <data.Montant) {
                    return Ok(new responseModel() {keyResponse= "solde_insuffisant", message = "le solde est insuffisant" });
                }

                Guid guid = Guid.NewGuid();
                string IdGuid= guid.ToString(); 

                _bdContext.Retraits.Add(new Retrait() {
                    Id= IdGuid,
                    Montant = data.Montant,
                    IdCompte = compte.Id  });
                var n = _bdContext.SaveChanges();
                
                if (n == 1)
                { return Ok(new responseModel() { message = "Retrait réussi" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error "  }); }
        }



        [HttpPost]
        [Route("ajouter_via_interbanque")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult ajouter_via_interbanque([FromBody] retraitViaInterbanqueSaveDTO data)
        {
             
            try
            {
                var banc = _bdContext.Interbanques.Where(item => item.Code == data.Code && item.Token == data.Token).FirstOrDefault();
                var compte = _bdContext.Comptes.Where(item => item.NumeroCompte == data.numeroCompte && item.Devise == data.Devise).FirstOrDefault();

                if (banc == null)
                { return Ok(new responseModel() {  keyResponse = "banque_notfound", message = "Veuillez vérifier les identités de la banque " }); }
                if (compte == null)
                { return Ok(new responseModel() {   keyResponse = "compte_notfound", message = "Ce compte n'est pas trouvé " }); }

                var sommeDepots = _bdContext.Depots.Where(item => item.IdCompte == compte.Id).Sum(Item => Item.Montant);
                var sommeRetraits = _bdContext.Retraits.Where(item => item.IdCompte == compte.Id).Sum(Item => Item.Montant);

                var solde = sommeDepots - sommeRetraits;
                if (solde < data.Montant)
                {
                    return Ok(new responseModel() { keyResponse = "solde_insuffisant", message = "le solde est insuffisant" });
                }

                Guid guid = Guid.NewGuid();
                string IdGuid = guid.ToString();

                _bdContext.Retraits.Add(new Retrait()
                {
                    Id = IdGuid,
                    Montant = data.Montant,
                    IdCompte = compte.Id,
                    FromInterbanque=banc.Id
                });
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { keyResponse = "reussi", message = "Retrait réussi" }); }
                else { return Ok(new responseModel() { keyResponse = "echec", message = "Echec" }); }
            }
            catch (Exception ex) { return Ok(new responseModel() {keyResponse="erreur", message = "Error " }); }
        }


    }
}
