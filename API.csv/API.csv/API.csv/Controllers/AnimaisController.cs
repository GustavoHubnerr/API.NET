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
        private Animal novoAnimal;

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

        [HttpPut("AlterarTudo")]
        public ActionResult<Animal> AlterarTudo(int id, [FromBody] Animal body)
        {
            Animal animal =
                _dbcontext
                .Animals.Find(a => a.Id == id);

            if (animal == null)
                return NotFound(); //404

            animal.Name = body.Name;
            animal.Classification = body.Classification;
            animal.Origin = body.Origin;
            animal.Reproduction = body.Reproduction;
            animal.Feeding = body.Feeding;

            return Ok(animal);
        }

        [HttpPost("Inserir")]
        public ActionResult<Animal> Inserir(int id, [FromBody] Animal body)
        {
            if (body == null)
                return BadRequest("os dados para a inserção não foram preenchidos"); //400


            Animal novoAnimal = new Animal();
            novoAnimal.Name = body.Name;
            novoAnimal.Id = _dbcontext.Animals.Max(a => a.Id) + 1;
            novoAnimal.Classification = body.Classification;
            novoAnimal.Origin = body.Origin;
            novoAnimal.Reproduction = body.Reproduction;
            novoAnimal.Feeding = body.Feeding;

            _dbcontext.Animals.Add(novoAnimal);

            return Ok(novoAnimal);
        }
        
    }
}
