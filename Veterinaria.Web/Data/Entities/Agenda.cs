namespace Veterinaria.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Agenda
    {
        public int Id { get; set; }
        [Display(Name = "Fecha")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Comentarios")]
        public string Remarks { get; set; }

        [Display(Name = "Esta disponible?")]
        public bool IsAvaible { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime DateLocal => Date.ToLocalTime();

        public Owner Owner { get; set; }
        public Pet Pet { get; set; }
    }
}
