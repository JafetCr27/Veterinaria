namespace Veterinaria.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Herlpers;

    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            this._context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1010", "Jafet", "Barrera", "jafetbarrera27@gmail.com", "8593 40 91", "Liberia Guanacaste", "Admin");
            var customer = await CheckUserAsync("1010", "Christian", "Madrigal", "cmadrigal@grupopelon.com", "8593 40 91", "Sarchí, Alajuela", "Customer");
            await CheckPetTypeAsync();
            await CheckServiceTypeAsync();
            await CheckOwnersAsync(customer);
            await CheckManagerAsync(manager);
            await CheckPetsAsync();
            await CheckAgendaAsync();

        }
        private async Task CheckManagerAsync(User manager)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = manager });
                await _context.SaveChangesAsync();
            };
        }

        private async Task<User> CheckUserAsync(string document, string name,
            string lastName, string email, string phone, string address, string rol)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user != null) return user;
            user = new User
            {
                FirstName = name,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document
            };
            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserAsync(user, rol);

            return user;
        }
        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }
        private async Task CheckPetsAsync()
        {
            if (!_context.Pets.Any())
            {
                var owner = _context.Owners.FirstOrDefault();
                var petType = _context.PetTypes.FirstOrDefault();
                AddPet("Max", owner, petType, "Zaguate");
                AddPet("Boby", owner, petType, "Doberman");
                await _context.SaveChangesAsync();

            }
        }
        private void AddPet(string name, Owner owner, PetType petType, string race)
        {
            _context.Pets.Add(
                new Pet
                {
                    Born = DateTime.Now.AddYears(-2),
                    Name = name,
                    Owner = owner,
                    PetType = petType,
                    Race = race
                });
        }
        private async Task CheckServiceTypeAsync()
        {
            if (!_context.ServiceTypes.Any())
            { 
                _context.ServiceTypes.Add(new ServiceType { Name = "Consulta" });
                _context.ServiceTypes.Add(new ServiceType { Name = "Vacunación" });
                _context.ServiceTypes.Add(new ServiceType { Name = "Urgencia" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckAgendaAsync()
        {
            if (!_context.Agendas.Any())
            {
                var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                var finalDate = initialDate.AddYears(1);
                while (initialDate < finalDate)
                {
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var finalDate2 = initialDate.AddHours(10);
                        while (initialDate < finalDate2)
                        {
                            _context.Agendas.Add(new Agenda
                            {
                                Date = initialDate.ToUniversalTime(),
                                IsAvaible = true
                            });

                            initialDate = initialDate.AddMinutes(30);
                        }

                        initialDate = initialDate.AddHours(14);
                    }
                    else
                    {
                        initialDate = initialDate.AddDays(1);
                    }
                }

                await _context.SaveChangesAsync();
            }

        }
        private async Task CheckOwnersAsync(User user)
        {
            if (!_context.Owners.Any())
            {
                _context.Owners.Add(new Owner { User = user });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckPetTypeAsync()
        {
            if (!_context.PetTypes.Any())
            {
                _context.PetTypes.Add(new PetType { Name = "Perro" });
                _context.PetTypes.Add(new PetType { Name = "Gato" });
                _context.PetTypes.Add(new PetType { Name = "Aves" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
