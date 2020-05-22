namespace Veterinaria.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class History
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [MaxLength(100)]
        public string Descripcion { get; set; }
        [Required]
        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Comentarios")]
        [MaxLength(255)]
        public string Remarks { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] public DateTime DateLocal => Date.ToLocalTime();

        public ServiceType ServiceType { get; set; }

        public Pet Pet { get; set; }
    }
}
