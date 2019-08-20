namespace Veterinaria.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class ServiceType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(50)]
        [Display(Name = "Tipo Servicio")]
        public string Name { get; set; }

        public ICollection<History> Histories { get; set; }
    }
}
