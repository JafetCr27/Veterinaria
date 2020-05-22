namespace Veterinaria.Web.Herlpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            var lista = _dataContext.PetTypes.Select(pt => new SelectListItem { Value = $"{pt.Id}", Text = pt.Name })
                .OrderBy(pt => pt.Text)
                .ToList();
            lista.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = " [Seleccione una opción]"
            });

            return lista;
        }

        public IEnumerable<SelectListItem> GetComboServiceTypes()
        {
            var lista = _dataContext.ServiceTypes.Select(pt => new SelectListItem { Value = $"{pt.Id}", Text = pt.Name })
                .OrderBy(pt => pt.Text)
                .ToList();
            lista.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = " [Seleccione una opción]"
            });
            return lista;
        }
    }
}
