namespace Veterinaria.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Owner
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Campo Obligatorio")]
        [MaxLength(30,ErrorMessage ="El campo {0} no puede contener mas de {1} caracteres.")]
        public string Document { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name ="Nombre")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name ="Apellido")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Teléfono fijo")]
        public string FixedPhone { get; set; }
        [MaxLength(20)]
        [Display(Name = "Celular")]
        public string CellPhone { get; set; }
        [MaxLength(200)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [Display(Name = "Propietario")]
        public string FullName => $"{FirstName} {LastName}";
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public ICollection<Agenda> Agendas { get; set; }
    }
}
