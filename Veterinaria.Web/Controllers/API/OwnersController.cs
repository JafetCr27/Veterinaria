namespace Veterinaria.Web.Controllers.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Veterinaria.Common.Models;
    using Veterinaria.Web.Data;
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OwnersController : ControllerBase
    {
        private readonly DataContext _context;

        //Inyecta la db
        public OwnersController(DataContext context)
        {
            _context = context;
        }
        // todos los get deben ser post por seguridad 
        [HttpPost]
        [Route("GetOwnerByEmail")]
        public async Task<IActionResult> GetOwner(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var owner = await _context.Owners
                .Include(o => o.User)
                .Include(o => o.Pets)
                .ThenInclude(o => o.PetType)
                .Include(o => o.Pets)
                .ThenInclude(p => p.Histories)
                .ThenInclude(h => h.ServiceType)
                .Include(o => o.Agendas)
                .FirstOrDefaultAsync(o => o.User.Email.ToLower() == emailRequest.Email.ToLower());
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(new OwnerResponse
            {
                Id = owner.Id,
                FirstName = owner.User.FirstName,
                LastName = owner.User.LastName,
                Address = owner.User.Address,
                Document = owner.User.Document,
                Email = owner.User.Email,
                PhoneNumber = owner.User.PhoneNumber,
                Pets = owner.Pets.Select(p => new PetResponse
                {
                    Id = p.Id,
                    Born = p.Born,
                    ImageUrl = p.ImageFullPath,
                    Name = p.Name,
                    Race = p.Race,
                    Remarks = p.Remarks,
                    PetType = p.PetType.Name,
                    Histories = p.Histories.Select(h => new HistoryResponse
                    {
                        Id = h.Id,
                        Remarks = h.Remarks,
                        ServiceType = h.ServiceType.Name,
                        Date = h.Date,
                        Description = h.Descripcion
                    }).ToList()
                }).ToList(),
            });
        }
    }
}
