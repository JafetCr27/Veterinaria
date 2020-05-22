
namespace Veterinaria.Web.Herlpers
{
    using Data.Entities;
    using Models;
    using System.Threading.Tasks;
    using Data;
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }
        public async Task<Pet> ToPetAsync(PetViewModel petViewModel, string path, bool isNew)
        {
            return new Pet
            {
                Agendas = petViewModel.Agendas,
                Born = petViewModel.Born,
                Histories = petViewModel.Histories,
                Id = isNew ? 0 : petViewModel.Id,
                ImageUrl = path,
                Name = petViewModel.Name,
                Owner = await _dataContext.Owners.FindAsync(petViewModel.OwnerId),
                PetType = await _dataContext.PetTypes.FindAsync(petViewModel.PetTypeId),
                Race = petViewModel.Race,
                Remarks = petViewModel.Remarks
            };
        }

        public PetViewModel ToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Agendas = pet.Agendas,
                Born = pet.Born,
                Histories = pet.Histories,
                ImageUrl = pet.ImageUrl,
                Name = pet.Name,
                Owner = pet.Owner,
                PetType = pet.PetType,
                Race = pet.Race,
                Remarks = pet.Remarks,
                Id = pet.Id,
                OwnerId = pet.Owner.Id,
                PetTypeId = pet.PetType.Id,
                PetTypes = _combosHelper.GetComboPetTypes()
            };
        }

        public async Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew)
        {
           return new History
           {
               Date = model.Date.ToUniversalTime(),
               Descripcion = model.Descripcion,
               Id = isNew ? 0:model.Id,
               Pet = await _dataContext.Pets.FindAsync(model.PetId),
               ServiceType = await _dataContext.ServiceTypes.FindAsync(model.ServiceTypeId),
               Remarks = model.Remarks
           };
        }

        public HistoryViewModel ToHistoryViewModel(History history)
        {
            return  new HistoryViewModel
            {
                Date = history.Date,
                Descripcion = history.Descripcion,
                Id = history.Id,
                PetId = history.Pet.Id,
                Remarks = history.Remarks,
                ServiceTypeId = history.ServiceType.Id,
                ServiceTypes = _combosHelper.GetComboServiceTypes(),
            };
        }
    }
}
