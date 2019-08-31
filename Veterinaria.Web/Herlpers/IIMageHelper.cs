
namespace Veterinaria.Web.Herlpers
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}
