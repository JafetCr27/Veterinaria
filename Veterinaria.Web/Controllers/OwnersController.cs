namespace Veterinaria.Web.Controllers
{
    using Data;
    using Data.Entities;
    using Herlpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    //[Authorize(Roles = "Admin")]
    public class OwnersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public OwnersController(
            DataContext context,
            IUserHelper userhelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper
            )
        {
            _context = context;
            _userHelper = userhelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_context.Owners
                    .Include(o => o.User)
                    .Include(o => o.Pets));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var owner = await _context.Owners
                    .Include(o => o.User)
                    .Include(o => o.Pets)
                    .ThenInclude(p => p.PetType)
                    .Include(o => o.Pets)
                    .ThenInclude(p => p.Histories)
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        public async Task<IActionResult> DetailsPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets
                .Include(p => p.Owner)
                .ThenInclude(o => o.User)
                .Include(p => p.Histories)
                .ThenInclude(h => h.ServiceType)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }
        public async Task<IActionResult> DeleteHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var history = await _context.Histories
                .Include(h => h.Pet)
                .FirstOrDefaultAsync(h => h.Id == id.Value);

            if (history == null)
            {
                return NotFound();
            }
            _context.Histories.Remove(history);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsPet)}/{history.Pet.Id}");
        }
        public async Task<IActionResult> DeletePet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets
                .Include(p => p.Owner)
                .Include(p => p.Histories)
                .FirstOrDefaultAsync(h => h.Id == id.Value);
            if (pet == null)
            {
                return NotFound();
            }
            if (pet.Histories.Count > 0 )
            {
                ModelState.AddModelError(string.Empty,"La mascota no puede borrarse porque tienes historias relacionadas" );
                return RedirectToAction($"{nameof(Details)}/{pet.Owner.Id}");

            }
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{pet.Owner.Id}");
        }
        // GET: Owners/Details/5
        public async Task<IActionResult> AddPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var owner = await _context.Owners.FindAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(new PetViewModel
            {
                Born = DateTime.Today,
                OwnerId = owner.Id,
                PetTypes = _combosHelper.GetComboPetTypes()
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddPet(PetViewModel petViewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                if (petViewModel.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(petViewModel.ImageFile);
                }
                var pet = await _converterHelper.ToPetAsync(petViewModel, path, true);
                _context.Pets.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{petViewModel.OwnerId}");
            }

            petViewModel.PetTypes = _combosHelper.GetComboPetTypes();
            return View(petViewModel);
        }
        public async Task<IActionResult> Editpet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets
                .Include(p => p.Owner)
                .Include(p => p.PetType)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToPetViewModel(pet));
        }

        [HttpPost]
        public async Task<IActionResult> Editpet(PetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;
                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                }
                var pet = await _converterHelper.ToPetAsync(model, path, false);
                _context.Pets.Update(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.OwnerId}");
            }

            model.PetTypes = _combosHelper.GetComboPetTypes();
            return View(model);
        }
        public async Task<IActionResult> AddHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }
            return View("_AddHistory", new HistoryViewModel
            {
                Date = DateTime.Now,
                PetId = pet.Id,
                ServiceTypes = _combosHelper.GetComboServiceTypes()
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddHistory(HistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var history = await _converterHelper.ToHistoryAsync(model, true);
                _context.Histories.Add(history);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsPet)}/{model.PetId}");
            }
            model.ServiceTypes = _combosHelper.GetComboServiceTypes();
            return View("_AddHistory", model);
        }


        public async Task<IActionResult> EditHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var history = await _context.Histories
                .Include(h => h.Pet)
                .Include(h => h.ServiceType)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (history == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToHistoryViewModel(history));
        }
        [HttpPost]
        public async Task<IActionResult> EditHistory(HistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var history = await _converterHelper.ToHistoryAsync(model, false);
                _context.Histories.Update(history);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsPet)}/{model.PetId}");
            }
            model.ServiceTypes = _combosHelper.GetComboServiceTypes();
            return View(model);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userHelper.AddUserAsync(new User
                {
                    Address = user.Address,
                    Document = user.Document,
                    Email = user.UserName,
                    FirstName = user.FirtsName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                }, user.Password);
                if (response.Succeeded)
                {
                    var userInDb = await _userHelper.GetUserByEmailAsync(user.UserName);
                    await _userHelper.AddUserToRoleAsync(userInDb, "Customer");
                    var owner = new Owner
                    {
                        Agendas = new List<Agenda>(),
                        Pets = new List<Pet>(),
                        User = userInDb,
                    };
                    _context.Owners.Add(owner);
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception e)
                    {

                        ModelState.AddModelError(string.Empty, e.ToString());
                        return View(user);
                    }
                }
                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault()?.Description);
            }
            return View(user);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var owner = await _context.Owners
                .Include(o=>o.User)
                .FirstOrDefaultAsync(o=>o.Id == id.Value);
            if (owner == null)
            {
                return NotFound();
            }
            var model = new EditUserViewModel
            {
                Address = owner.User.Address,
                Document = owner.User.Document,
                FirtsName = owner.User.FirstName,
                Id = owner.Id,
                LastName = owner.User.LastName,
                PhoneNumber = owner.User.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
          
            if (ModelState.IsValid)
            {
                var owner = await _context.Owners
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o=>o.Id == model.Id);
                owner.User.Document = model.Document;
                owner.User.FirstName = model.FirtsName;
                owner.User.LastName = model.LastName;
                owner.User.Address = model.Address;
                owner.User.PhoneNumber = model.PhoneNumber;
                await _userHelper.UpdateUserAsync(owner.User);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();  
            }
            var owner = await _context.Owners
                .Include(o=>o.User)
                .Include(o=>o.Pets)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (owner == null)
            {
                return NotFound();
            }
            if (owner.Pets.Count > 0)
            {
                ModelState.AddModelError(string.Empty,"No se pudo eliminar el propietario");
                return RedirectToAction(nameof(Index));
            }
            await _userHelper.DeleteUserAsync(owner.User.Email);
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }
    }
}
