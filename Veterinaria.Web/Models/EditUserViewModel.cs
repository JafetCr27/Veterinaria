using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Web.Models
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "{0} solo permite 20 caracteres")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string Document { get; set; }
        [Display(Name = "Nombre:")]
        [Required]
        [MaxLength(20, ErrorMessage = "El {0} no puede tener mas de {1} caracteres")]
        public string FirtsName { get; set; }
        [Display(Name = "Apellido:")]
        [Required]
        [MaxLength(50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres")]
        public string LastName { get; set; }
        [Display(Name = "Dirección:")]
        [Required]
        [MaxLength(50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres")]
        public string Address { get; set; }

        [Display(Name = "Teléfono:")]
        [Required]
        [MaxLength(50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres")]
        public string PhoneNumber { get; set; }

    }
}
