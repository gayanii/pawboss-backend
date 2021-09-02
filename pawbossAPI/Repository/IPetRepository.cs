using pawbossAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pawbossAPI.Repository
{
    public interface IPetRepository
    {
            IEnumerable<Pet> GetPets();
            List<PetDetails> GetAdoptedPets();
            IEnumerable<Pet> GetNotAdoptedPets();
            bool CreatePet(Pet pet);
            bool UpdatePet(PetUpdate pet);
            bool DeletePet(int id);
            Pet GetPet(int id);
    }
}

