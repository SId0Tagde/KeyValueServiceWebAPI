using assigntwo.Data;
using assigntwo.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assigntwo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepository repository;
        private readonly LinkGenerator linkgenerator;

        public MatchController(IMatchRepository repository,LinkGenerator linkgenerator)
        {
            this.repository = repository;
            this.linkgenerator = linkgenerator;
        }

        [HttpGet("keys/{key}")]
        public ActionResult<Match> Get(string key)
        {  
            //Whether match element exists, if not return StatusCode404
            if (repository.Exist(key))
            {
                var match = repository.GetMatchbykey(key);
                return Ok(match);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Specified key does not exist");
            }
        }

        [HttpPost("keys")]
        // { /api/[controller]/keys }
        public async Task<ActionResult<Match>> Post([FromBody] Match match)
        {   
            //Whether Match element exists
            if (repository.Exist(match.Key))
            {
                return StatusCode(StatusCodes.Status409Conflict, "Specified Key already exist");
            }

            //Add Match element to the repository
            repository.Add(match);

            //Save changes to the repository  
            if (await repository.SaveChanges())
            {
                var location = linkgenerator.GetPathByAction("Get", "Match", new { key = match.Key });
                return Created(location!, match);
            }
            else
            {
                return BadRequest("Unable to save changes");
            }
        }



        [HttpPatch("keys/{key}/{value}")]
        public async Task<IActionResult> Patch(string key, int value)
        {
            //Whether the Match element with specified key exists
            if (repository.Exist(key))
            {
             //update that Match element with specified value 
             repository.Update(key, value);
                //Save changes
                if (await repository.SaveChanges())
                {
                    return Ok();
                }
            }
            //if key does not exist
            return StatusCode(StatusCodes.Status404NotFound, "Specified Key does not exist");
        }

        [HttpDelete("keys/{key}")]
        public async Task<IActionResult> Delete(string key)
        {   
            //Whether Match element exist, if not return StatusCode404
            if(repository.Exist(key))
            {   
                //delete that Match element
                repository.Delete(repository.GetMatchbykey(key));
                //Save changes
                if (await repository.SaveChanges())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest($"Failed to delete {key} and save changes");
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, " Key does not exist");
            }

        }
    }
}
