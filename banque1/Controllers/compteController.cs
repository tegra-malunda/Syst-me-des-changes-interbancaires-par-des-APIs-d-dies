using Microsoft.AspNetCore.Mvc; 
using System.Data;
using banque1.Models;
using banque1.DTO.compte;
using banque1.DTO;

namespace banque1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompteController : ControllerBase
    {
        private Banque1Context _bdContext;
        private readonly IConfiguration _conf;
        public CompteController(Banque1Context bdContext, IConfiguration conf)
        {
            _bdContext = bdContext;
            _conf = conf;
        }

        [HttpGet]
        [Route("liste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<compteReturnDTO>> GetList()
        {


            var liste = _bdContext.Comptes.Select(item => new compteReturnDTO() { Id = item.Id,
                Nom = item.Nom,
                Postnom = item.Postnom,
                Prenom = item.Prenom,
                Sexe = item.Sexe,
                Telephone = item.Telephone,
                Email = item.Email,
                Adresse = item.Adresse,
                Devise = item.Devise,
                NumeroCompte = item.NumeroCompte
            }).OrderBy(Item => Item.Nom).ThenBy(Item => Item.Postnom).ThenBy(Item => Item.Prenom).ToList();

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
                string IdGuid = guid.ToString();
                Random random = new Random();
                int number = random.Next(1000, 10000);  // Génère un nombre entre 1000 et 9999
                string formattedNumber = number.ToString("D4");

                _bdContext.Comptes.Add(new Compte() {
                    Id = IdGuid,
                    Nom = data.Nom.Trim(),
                    Postnom = data.Postnom.Trim(),
                    Prenom = data.Prenom.Trim(),
                    Sexe = data.Sexe.Trim().ToUpper(),
                    Telephone = data.Telephone.Trim(),
                    Email = data.Email.Trim(),
                    Adresse = data.Adresse,
                    Devise = data.Devise,
                    CodePin = formattedNumber,
                    NumeroCompte = DateTime.Now.ToString("yyyyMMddHHmmss") + (data.Nom.Trim() + data.Postnom.Trim() + data.Prenom.Trim()).Length
                });
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Enregistrement réussi" }); }
                else { return BadRequest(new responseModel() { message = "Enregistrement échoué" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " }); }
        }

        [HttpPost]
        [Route("modifier_codepin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult modifier_codepin([FromBody] compteUpdatePinDTO data)
        {
            try
            {
                var compte = _bdContext.Comptes.Where(item => item.NumeroCompte == data.Numero_compte && item.CodePin == data.Code_pin_old).FirstOrDefault();
                if (compte == null)
                { return NotFound(new responseModel() { message = "Veuillez vous rassurer que le numéro de compte ou le code pin est correct" }); }

                compte.CodePin = data.Code_pin_new;
                _bdContext.Comptes.Update(compte);
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Code pin modifié avec succès" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " + ex }); }
        }

        [HttpPost]
        [Route("solde")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult solde([FromBody] compteSoldeDTO data)
        {
            try
            {
                var compte = _bdContext.Comptes.Where(item => item.NumeroCompte == data.numero_compte).FirstOrDefault();
                if (compte == null)
                { return NotFound(new responseModel() { message = "Ce numéro de compte n'existe pas" }); }

                var sommeDepots = _bdContext.Depots.Where(item => item.IdCompte == compte.Id).Sum(Item => Item.Montant);
                var sommeRetraits = _bdContext.Retraits.Where(item => item.IdCompte == compte.Id).Sum(Item => Item.Montant);

                var solde = sommeDepots - sommeRetraits;

              return  Ok (new { solde = solde, devise = compte.Devise } );
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " + ex }); }
        }

    }
}
