namespace Veterinaria.Web.Herlpers
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models;
    public interface IConverterHelper
    {
        Task<Pet> ToPetAsync(PetViewModel petViewModel, string path, bool isNew);
        PetViewModel ToPetViewModel(Pet pet);

        Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew);

        HistoryViewModel ToHistoryViewModel(History history);
    }
}
