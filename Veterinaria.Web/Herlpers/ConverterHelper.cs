
namespace Veterinaria.Web.Herlpers
{
    using Data.Entities;
    using Models;
    using System.Threading.Tasks;
    using Data;
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;

        public ConverterHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Pet> ToPetAsync(PetViewModel petViewModel, string path)
        {
            var pet = new Pet
            {
                Agendas = petViewModel.Agendas,
                Born = petViewModel.Born,
                Histories = petViewModel.Histories,
               // Id = petViewModel.Id == 0 ? null : petViewModel.Id,
                ImageUrl = path,
                Name = petViewModel.Name,
                Owner = await _dataContext.Owners.FindAsync(petViewModel.OwnerId),
                PetType = await _dataContext.PetTypes.FindAsync(petViewModel.PetTypeId),
                Race = petViewModel.Race,
                Remarks = petViewModel.Remarks
            };
            if (petViewModel.Id != 0)
            {
                pet.Id = petViewModel.Id;
            }
            return pet;
        }
    }
}
