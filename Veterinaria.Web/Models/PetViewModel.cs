namespace Veterinaria.Web.Models
{
    using Data.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class PetViewModel : Pet
    {
        public int OwnerId { get; set; }
        [Required]
        [Display(Name = "Tipo Mascota:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de mascota")]
        public int PetTypeId { get; set; }

        public IFormFile ImageFile { get; set; }

        public IEnumerable<SelectListItem> PetTypes { get; set; }
    }
}
