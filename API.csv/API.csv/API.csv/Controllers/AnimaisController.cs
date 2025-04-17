using API.csv.DataBase;
using API.csv.DataBase.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.csv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaisController : ControllerBase
    {
        private DBContext _dbcontext;

        public AnimaisController(DBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public ActionResult<List<Animal>> GetAll()
        {
            return Ok(_dbcontext.Animals);
        }
        [HttpGet("{id}")]
        public ActionResult<Animal> GetById(int id)
        {
            try
            {
                Animal animal =
                    _dbcontext
                    .Animals.Find(a => a.Id == id);

                if (animal == null)
                    return NotFound();//404;

                return Ok(animal);//200
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);//400
            }
            
        }
        [HttpDelete("{id}")]
        public ActionResult<Animal> Delete(int id)
        {
            try
            {
                Animal animal =
                    _dbcontext
                    .Animals.Find(a => a.Id == id);

                if (animal == null)
                    return NotFound();//404;

                _dbcontext.Animals.Remove(animal); //204
                return NoContent();
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);//400
            }

        }
        [HttpPatch("AlterarNome")]
        public ActionResult<Animal> AlterarNome(int id, [Frombody] Animal body)
        {
            if ((body == null) ||
                    (string.IsNullOrEmpty(body.Name)))
                return BadRequest();

            Animal animal =
                 _dbcontext
                 .Animals.Find(a => a.Id == body.Id);

            if (animal == null)
                return NotFound();
            animal.Name = body.Name;
            return Ok(animal);

        }

    }
}
