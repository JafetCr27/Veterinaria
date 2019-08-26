using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Cédula")]
        public string Document { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [MaxLength(200)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [Display(Name = "Propietario")]
        public string FullName => $"{FirstName} {LastName}";
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}
