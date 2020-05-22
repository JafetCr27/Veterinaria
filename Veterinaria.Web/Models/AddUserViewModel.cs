using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Web.Models
{
    public class AddUserViewModel: EditUserViewModel
    {
        [Display(Name = "Email:")]
        [Required]
        [MaxLength(100,ErrorMessage = "El {0} no puede tener mas de {1} caracteres")]
        [EmailAddress]
        public string UserName { get; set; }

        [Display(Name = "Contraseña:")]
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength = 6,ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña:")]
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }


    }
}
