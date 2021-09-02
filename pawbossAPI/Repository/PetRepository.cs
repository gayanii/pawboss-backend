using pawbossAPI.DBContexts;
using pawbossAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static pawbossAPI.Enum;

namespace pawbossAPI.Repository
{
    public class PetRepository : IPetRepository
    {
        private PawBossContext db = new PawBossContext();

        public bool CreatePet(Pet pet)
        {
            var identifyUser = db.User.Where(x => x.Id.Equals(pet.FoundedById)).FirstOrDefault();
            identifyUser.FoundedPetsCount = identifyUser.FoundedPetsCount + 1; // Increasing founded pet count
            identifyUser.Points = identifyUser.Points + 1; // Increasing points
            db.Add(pet);
            db.SaveChanges();
            return true;
        }

        public bool DeletePet(int id)
        {
            var deletePet = db.Pet.Where(x => x.Id.Equals(id)).FirstOrDefault();
            db.Remove(deletePet);
            db.SaveChanges();
            return true;
        }

        public Pet GetPet(int id)
        {
            try
            {
                var pet = db.Pet.Where(x => x.Id.Equals(id)).FirstOrDefault();
                if (pet != null) return pet;
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public IEnumerable<Pet> GetPets()
        {
            var allPets = db.Pet.ToList();
            return allPets;
        }

        public List<PetDetails> GetAdoptedPets()
        {
            List<PetDetails> allAdopted = new List<PetDetails>();
            var allAdoptedPets = db.Pet.Where(x => x.IsAdopted.Equals(true)).ToList();

            for (int i = 0; i < allAdoptedPets.Count(); i++)
            {
                var userDetails = db.User.First(x => x.Id.Equals(allAdoptedPets[i].AdopterId));
                PetDetails pet = new PetDetails()
                {
                    Id = allAdoptedPets[i].Id,
                    Area = allAdoptedPets[i].Area,
                    FoundedOn = allAdoptedPets[i].FoundedOn,
                    IsAdopted = allAdoptedPets[i].IsAdopted,
                    IdentityPhoto = allAdoptedPets[i].IdentityPhoto,
                    AdopterId = allAdoptedPets[i].AdopterId != null ? allAdoptedPets[i].AdopterId : null,
                    FoundedById = allAdoptedPets[i].FoundedById,
                    AdoptedUser = userDetails.Username,
                    Description = allAdoptedPets[i].Description,
                };
                allAdopted.Add(pet);
            }
            return allAdopted;
        }

        public IEnumerable<Pet> GetNotAdoptedPets()
        {
            var allNotAdoptedPets = db.Pet.Where(x => x.IsAdopted.Equals(false)).ToList();
            return allNotAdoptedPets;
        }

        public bool UpdatePet(PetUpdate pet)
        {
            var updatingPet = db.Pet.First(x => x.Id.Equals(pet.Id));
            updatingPet.Area = pet.Area;
            updatingPet.Description = pet.Description;
            updatingPet.IsAdopted = pet.IsAdopted;
            updatingPet.AdopterId = pet.AdopterId;
            updatingPet.IsAdopted = Convert.ToBoolean((int)AdoptedStatus.Addopted);

            var identifyUser = db.User.Where(x => x.Id.Equals(pet.AdopterId)).FirstOrDefault();
            identifyUser.AdoptedPetsCount = identifyUser.AdoptedPetsCount + 1; // Increasing adopted pet count
            identifyUser.Points = identifyUser.Points + 1; // Increasing points
            db.SaveChanges();
            return true;
        }
    }
}
