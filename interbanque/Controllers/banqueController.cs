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
    public class BanqueController : ControllerBase
    {
        private InterbanqueContext _bdContext;
        private readonly IConfiguration _conf;
        public BanqueController(InterbanqueContext bdContext, IConfiguration conf)
        {
            _bdContext = bdContext;
            _conf = conf;
        }

        [HttpGet]
        [Route("liste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ActionResult<IEnumerable<banqueReturnDTO>> GetList( )
        {
           

            var liste = _bdContext.Banques.Select(item => new  banqueReturnDTO()  { Id = item.Id,
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
        public ActionResult ajouter([FromBody]  banqueSaveDTO data)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string IdGuid= guid.ToString();
                Random random = new Random();
                int number = random.Next(1000, 10000);  // Génère un nombre entre 1000 et 9999
                string formattedNumber = number.ToString("D4");

                _bdContext.Banques.Add(new  Banque() {
                    Id= IdGuid,
                    Nom = data.Nom.Trim(),
                    Code = data.Code.Trim(),
                    Token = Cryptage.crypter(data.Token.Trim(), _conf)
                ,
                    EndpointDepot = Cryptage.crypter(data.EndpointDepot.Trim(), _conf)
                ,
                    EndpointRetrait = Cryptage.crypter(data.EndpointRetrait.Trim(), _conf)
                });
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
        public ActionResult modifier_code([FromBody]  banqueUpdateCodeDTO data)
        {
            try
            {
                var bq = _bdContext.Banques.Where(item => item.Id == data.id && item.Code == data.Code_old).FirstOrDefault();
                if (bq == null)
                { return NotFound(new responseModel() { message = "Veuillez vous rassurer si le code ou l'id  banque sont corrects" }); }

                bq.Code = data.Code_new;
                _bdContext.Banques.Update(bq);
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
        public ActionResult modifier_token([FromBody]  banqueUpdateTokenDTO data)
        {
            try
            {
                var interbq = _bdContext.Banques.Where(item => item.Id == data.id && Cryptage.crypter(data.Token_old,_conf) == item.Token).FirstOrDefault();
                if (interbq == null)
                { return NotFound(new responseModel() { message="Veuillez vous rassurer si le token ou l'id  banque sont corrects" }); }

                interbq.Token = Cryptage.crypter(data.Token_new.Trim(), _conf)  ;
                _bdContext.Banques.Update(interbq);
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Token modifié avec succès" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " + ex }); }
        }

        [HttpPost]
        [Route("modifier_endpoint_depot")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult modifier_endpoint_depot([FromBody] banqueUpdateEnpointDepotDTO data)
        {
            try
            {
                var bq = _bdContext.Banques.Where(item => item.Id == data.id ).FirstOrDefault();
                if (bq == null)
                { return NotFound(new responseModel() { message = "Veuillez vous rassurer que l'id  banque sont corrects" }); }

                bq.EndpointDepot =  Cryptage.crypter(data.EnpointDepot, _conf);
                _bdContext.Banques.Update(bq);
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Endpoint depot modifié avec succès" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " + ex }); }
        }


        [HttpPost]
        [Route("modifier_endpoint_retrait")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult modifier_endpoint_retrait([FromBody] banqueUpdateEnpointRetraitDTO data)
        {
            try
            {
                var bq = _bdContext.Banques.Where(item => item.Id == data.id).FirstOrDefault();
                if (bq == null)
                { return NotFound(new responseModel() { message = "Veuillez vous rassurer que l'id  banque sont corrects" }); }

                bq.EndpointRetrait =Cryptage.crypter( data.EnpointRetrait,_conf);
                _bdContext.Banques.Update(bq);
                var n = _bdContext.SaveChanges();

                if (n == 1)
                { return Ok(new responseModel() { message = "Endpoint retrait modifié avec succès" }); }
                else { return BadRequest(new responseModel() { message = "Echec" }); }
            }
            catch (Exception ex) { return BadRequest(new responseModel() { message = "Error " + ex }); }
        }

    }
}
