using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Veterinaria.Web.Herlpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPetTypes();
    }
}