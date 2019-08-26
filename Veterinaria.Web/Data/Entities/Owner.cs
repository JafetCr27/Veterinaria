namespace Veterinaria.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Owner
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede contener mas de {1} caracteres.")]
        public User User{ get; set; }
        public ICollection<Agenda> Agendas { get; set; }
        public ICollection<Pet> Pets{ get; set; }
    }
}
