using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pawbossAPI.Entities;
using pawbossAPI.Repository;


namespace pawbossAPI.Controllers
{

    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPetRepository _petRepository;
        public PetsController(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            this.mapper = mapper;

        }

        // http://localhost:53024/api/pets
        [Route("getAllPets"), HttpGet()]
        public IActionResult GetAll()
        {
            var allPets = _petRepository.GetPets();
            return Ok(allPets);
        }

        // http://localhost:53024/api/pets/getAdopted
        [Route("getAdopted"), HttpGet]
        public IActionResult GetAdopted()
        {
            var allAdoptedPets = _petRepository.GetAdoptedPets();
            return Ok(allAdoptedPets);
        }

        // http://localhost:53024/api/pets/getNotAdopted
        [Route("getNotAdopted"), HttpGet]
        public IActionResult GetNotAdopted()
        {
            var allNotAdoptedPets = _petRepository.GetNotAdoptedPets();
            return Ok(allNotAdoptedPets);
        }

        // http://localhost:53024/api/pets
        [HttpPost]
        public IActionResult Create([FromBody] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addingPet = mapper.Map<Pet>(pet);


            var result = _petRepository.CreatePet(addingPet);

            if (!result)
            {
                return BadRequest();
            }

            else
            {
                return Ok(addingPet);
            }
        }

        // http://localhost:53024/api/pets
        [HttpPut]
        public IActionResult UpdateStatus([FromBody] PetUpdate pet)
        {
            var result = _petRepository.UpdatePet(pet);
            return Ok();
        }

        // http://localhost:53024/api/pets/1
        [Route("{id}"), HttpDelete]
        public IActionResult DeletePermanetly(int id)
        {
            var result = _petRepository.DeletePet(id);
            return Ok();
        }

        // http://localhost:53024/api/pets/getPet
        [Route("getPet"), HttpPost]
        public IActionResult GetPet([FromBody] inputId pet)
        {
            var getPet = _petRepository.GetPet(pet.Id);

            if (getPet == null)
            {
                return NotFound();
            }
            return Ok(getPet);
        }
    }
}

