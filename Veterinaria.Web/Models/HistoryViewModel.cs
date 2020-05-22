namespace Veterinaria.Web.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;
    public class HistoryViewModel : History
    {
        public int PetId { get; set; }
        [Display(Name = "Tipo de servicio")]
        [Range(1,int.MaxValue,ErrorMessage = "Debe seleccionar un tipo de servicio")]
        public int ServiceTypeId { get; set; }

        public IEnumerable<SelectListItem> ServiceTypes { get; set; }
    }
}
