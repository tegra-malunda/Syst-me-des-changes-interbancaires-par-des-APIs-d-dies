using Microsoft.AspNetCore.Mvc;
using banque2.DTO;
using System.Data;
using banque2.Models;
using banque2.DTO.compte;
using banque2.DTO.interbanque;
using banque2.Services;

namespace banque2.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class InterbanqueController : ControllerBase
    {
        private Banque2Context _bdContext;
        private readonly IConfiguration _conf;
        public InterbanqueController(Banque2Context bdContext, IConfiguration conf)
        {
            _bdContext = bdContext;
            _conf = conf;
        }

        [HttpGet]
        [Route("liste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult<IEnumerable<interbanqueReturnDTO>> GetList( )
        {
           

            var liste = _bdContext.Interbanques.Select(item => new interbanqueReturnDTO()  { Id = item.Id,
                Nom = item.Nom,
                Code=item.Code
                
            }).OrderBy(Item => Item.Nom).ToList();
            
             return Ok(liste); 
        }



        

        [HttpPost]
        [Route("ajouter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult ajouter([FromBody] interbanqueSaveDTO data)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string IdGuid= guid.ToString();
                Random random = new Random();
                int number = random.Next(1000, 10000);  // Génère un nombre entre 1000 et 9999
                string formattedNumber = number.ToString("D4");

                _bdContext.Interbanques.Add(new Interbanque() {
                    Id= IdGuid,
                    Nom = data.Nom.Trim(),
                    Code = data.Code.Trim(),
                    Token = Cryptage.crypter(data.Token.Trim(), _conf)    });
                var n = _bdContext.SaveChanges();
                
                if (n == 1)
                { return Ok(new responseModel() { message = "Enregistrement réussi" }); }
                else { return BadRequest(new responseModel() { message = "Enregistrement échoué" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error "  }); }
        }

        [HttpPost]
        [Route("modifier_code")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult modifier_code([FromBody] InterbanqueUpdateCodeDTO data)
        {
            try
            {
                var interbq = _bdContext.Interbanques.Where(item => item.Id == data.id && item.Code == data.Code_old).FirstOrDefault();
                if (interbq == null)
                { return NotFound(new responseModel() { message = "Veuillez vous rassurer si le code ou l'id interbanque sont corrects" }); }

                interbq.Code = data.Code_new;
                _bdContext.Interbanques.Update(interbq);
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Code modifié avec succès" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error "+ex }); }
        }


        [HttpPost]
        [Route("modifier_token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult modifier_token([FromBody] InterbanqueUpdateTokenDTO data)
        {
            try
            {
                var interbq = _bdContext.Interbanques.Where(item => item.Id == data.id && Cryptage.crypter(data.Token_old,_conf) == item.Token).FirstOrDefault();
                if (interbq == null)
                { return NotFound(new responseModel() { message="Veuillez vous rassurer si le token ou l'id interbanque sont corrects" }); }

                interbq.Token = Cryptage.crypter(data.Token_new.Trim(), _conf)  ;
                _bdContext.Interbanques.Update(interbq);
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Token modifié avec succès" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " + ex }); }
        }


    }
}
