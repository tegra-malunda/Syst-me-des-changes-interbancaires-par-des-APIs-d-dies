using Microsoft.AspNetCore.Mvc; 
using System.Data;
using interbanque.Models; 
using interbanque.DTO;
using interbanque.DTO.client;
using interbanque.DTO.compte;
using interbanque.Services; 

namespace banque1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class clientController : ControllerBase
    {
        private InterbanqueContext _bdContext;
        private readonly IConfiguration _conf;
        public clientController(InterbanqueContext bdContext, IConfiguration conf)
        {
            _bdContext = bdContext;
            _conf = conf;
        }

        [HttpGet]
        [Route("liste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<clientReturnDTO>> GetList()
        {


            var liste = _bdContext.Clients.Select(item => new clientReturnDTO() { Id = item.Id,
                Nom = item.Nom,
                Postnom = item.Postnom,
                Prenom = item.Prenom,
                Sexe = item.Sexe,
                Telephone = item.Telephone,
                Email = item.Email, 
               
            }).OrderBy(Item => Item.Nom).ThenBy(Item => Item.Postnom).ThenBy(Item => Item.Prenom).ToList();

            return Ok(liste);
        }





        [HttpPost]
        [Route("ajouter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult ajouter([FromBody] clientSaveDTO data)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string IdGuid = guid.ToString();
             

                _bdContext.Clients.Add(new Client() {
                    Id = IdGuid,
                    Nom = data.Nom.Trim(),
                    Postnom = data.Postnom.Trim(),
                    Prenom = data.Prenom.Trim(),
                    Sexe = data.Sexe.Trim().ToUpper(),
                    Telephone = data.Telephone.Trim(),
                    Email = data.Email.Trim(),
                    Motpasse =Cryptage.crypter(data.Motpasse,_conf)
                });
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Enregistrement réussi" }); }
                else { return BadRequest(new responseModel() { message = "Enregistrement échoué" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " }); }
        }

        [HttpPost]
        [Route("modifier_motpasse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult modifier_motpasse([FromBody] clientUpdateMotpasseDTO data)
        {
            try
            {
                var client = _bdContext.Clients.Where(item => item.Id == data.Id && Cryptage.crypter(data.motpasse_old,_conf) == item.Motpasse).FirstOrDefault();
                if (client == null)
                { return NotFound(new responseModel() { message = "Veuillez vous rassurer si l'ancien mot de passe est correct" }); }

                client.Motpasse = data.motpasse_new;
                _bdContext.Clients.Update(client);
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Mot de passe modifié avec succès" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " + ex }); }
        }
 
    }
}
