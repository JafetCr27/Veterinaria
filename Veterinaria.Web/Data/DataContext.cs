using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Web.Data.Entities;

namespace Veterinaria.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        internal Task CheckPetsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
