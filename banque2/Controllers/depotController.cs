using Microsoft.AspNetCore.Mvc; 
using System.Data;
using banque2.Models;
using banque2.DTO.depot;
using banque2.DTO;

namespace banque2.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class DepotController : ControllerBase
    {
        private Banque2Context _bdContext;
        private readonly IConfiguration _conf;
        public DepotController(Banque2Context bdContext, IConfiguration conf)
        {
            _bdContext = bdContext;
            _conf = conf;
        }

        [HttpGet]
        [Route("liste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult<IEnumerable<depotReturnDTO>> GetList( )
        {
           

            var liste =(from depot in _bdContext.Depots
                       join compte in _bdContext.Comptes on depot.IdCompte equals compte.Id
                 select new depotReturnDTO()  { Id = depot.Id, 
                 FromInterbanque=depot.FromInterbanque,
                 IdCompte=depot.IdCompte,
                 numeroCompte=compte.NumeroCompte,
                 Montant = depot.Montant,
                 Devise=compte.Devise,
                 Date= depot.Date
            }).OrderBy(Item => Item.Date).ToList();
            
             return Ok(liste); 
        }



        

        [HttpPost]
        [Route("ajouter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult ajouter([FromBody] depotSaveDTO data)
        {

            try
            {
                var compte = _bdContext.Comptes.Where(item => item.NumeroCompte == data.numeroCompte).FirstOrDefault();
                if (compte == null)
                { return Ok(new responseModel() { keyResponse = "compte_notfound", message = "Ce numéro de compte n'existe pas" }); }

                Guid guid = Guid.NewGuid();
                string IdGuid= guid.ToString(); 

                _bdContext.Depots.Add(new Depot() {
                    Id= IdGuid,
                    Montant = data.Montant,
                    IdCompte = compte.Id  });
                var n = _bdContext.SaveChanges();
                
                if (n == 1)
                { return Ok(new responseModel() { message = "Dépot réussi" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error "  }); }
        }



        [HttpPost]
        [Route("ajouter_via_interbanque")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult ajouter_via_interbanque([FromBody] depotViaInterbanqueSaveDTO data)
        {

            try
            {
                var banc = _bdContext.Interbanques.Where(item => item.Code == data.Code && item.Token == data.Token).FirstOrDefault();
                var compte = _bdContext.Comptes.Where(item => item.NumeroCompte == data.numeroCompte && item.Devise == data.Devise).FirstOrDefault();

                if (banc == null)
                { return Ok(new responseModel() { keyResponse = "banque_notfound", message = "Veuillez vérifier les identités de la banque " }); }
                if (compte == null)
                { return Ok(new responseModel() { keyResponse = "compte_notfound", message = "Ce compte n'est pas trouvé " }); }



                Guid guid = Guid.NewGuid();
                string IdGuid = guid.ToString();

                _bdContext.Depots.Add(new Depot()
                {
                    Id = IdGuid,
                    Montant = data.Montant,
                    IdCompte = compte.Id,
                    FromInterbanque=banc.Id
                });
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { keyResponse = "reussi", message = "Dépot réussi" }); }
                else { return Ok(new responseModel() { keyResponse = "echec", message = "Echec" }); }
            }
            catch (Exception ex) { return Ok(new responseModel() {keyResponse="erreur", message = "Error " }); }
        }


    }
}
