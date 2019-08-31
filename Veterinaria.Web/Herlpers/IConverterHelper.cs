using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Web.Data.Entities;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Herlpers
{
    public interface IConverterHelper
    {
        Task<Pet> ToPetAsync(PetViewModel petViewModel, string path);
    }
}
