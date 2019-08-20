namespace Veterinaria.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Veterinaria.Web.Data.Entities;

    public class SeedDb
    {
        private DataContext _context { get; set; }

        public SeedDb(DataContext context)
        {
            this._context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckPetTypeAsync();
            await CheckServiceTypeAsync();
            await CheckOwnersAsync();
            await CheckPetsAsync();
            await CheckAgendaAsync();

        }

        private async Task CheckPetsAsync()
        {
            var owner = _context.Owners.FirstOrDefault();
            var petType = _context.PetTypes.FirstOrDefault();
            if (!_context.Pets.Any())
            {
                AddPet("Max", owner, petType, "Zaguate");
                AddPet("Boby", owner, petType, "Doberman");
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

        private async Task CheckOwnersAsync()
        {
            if (!_context.Owners.Any())
            {
                AddOwner(new Owner { Document = "116020305", FirstName = "Jafeth", LastName = "Barrera", FixedPhone = "N/A", CellPhone = "85934091", Address = "Liberia" });
                AddOwner(new Owner { Document = "116560399", FirstName = "Christian ", LastName = "Madrigal", FixedPhone = "N/A", CellPhone = "85934091", Address = "Sarchí" });
                AddOwner(new Owner { Document = "11687428R", FirstName = "Juan", LastName = "Perez", FixedPhone = "N/A", CellPhone = "85934091", Address = "Liberia" });
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(Owner owner)
        {
            _context.Add(owner);
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
