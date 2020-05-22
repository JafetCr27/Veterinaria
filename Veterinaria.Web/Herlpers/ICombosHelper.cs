namespace Veterinaria.Web.Herlpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using Data.Entities;
    using Models;
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPetTypes();
        IEnumerable<SelectListItem> GetComboServiceTypes();

    }
}