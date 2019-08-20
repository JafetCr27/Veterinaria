namespace Veterinaria.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class PetType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(50)]
        [Display(Name = "Tipo Mascota")]
        public string Name { get; set; }

        //Relacion de 1 a muchos
        public ICollection<Pet> Pets { get; set; }
    }
}
