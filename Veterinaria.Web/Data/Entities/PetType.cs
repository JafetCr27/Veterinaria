using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Web.Data.Entities
{
    public class PetType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(50)]
        [Display(Name = "Tipo Mascota")]
        public string Name { get; set; }
    }
}
