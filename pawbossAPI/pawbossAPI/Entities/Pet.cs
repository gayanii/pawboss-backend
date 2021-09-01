using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pawbossAPI.Entities
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }
        public string Area { get; set; }
        public DateTime FoundedOn { get; set; }
        public bool IsAdopted { get; set; }
        public string IdentityPhoto { get; set; }
        [ForeignKey("AdopterId")]
        public int? AdopterId { get; set; }
        [ForeignKey("FoundedById")]
        [Required]
        public int FoundedById { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class inputId
    {
        public int Id { get; set; }
    }

    public class PetUpdate
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Area { get; set; }
        [Required]
        public bool IsAdopted { get; set; }
        [Required]
        public int AdopterId { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class PetDetails
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public DateTime FoundedOn { get; set; }
        public bool IsAdopted { get; set; }
        public string IdentityPhoto { get; set; }
        public int? AdopterId { get; set; }
        public int FoundedById { get; set; }
        public string AdoptedUser { get; set; }
        public string Description { get; set; }
    }
}
