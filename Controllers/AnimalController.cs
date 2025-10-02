using ApiTrivIA.Models;
using ApiTrivIA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTrivIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnimalController : ControllerBase
    {
            private readonly AnimalService _animalService;

        public AnimalController(AnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        [Route("ListarAnimales")]
        public async Task<ActionResult<List<Animal>>> listarAnimal()
        {
            return Ok(await _animalService.ListarAnimales());
        }


    }
}

