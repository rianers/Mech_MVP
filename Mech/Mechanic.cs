using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Mech
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    class MechanicDb : DbContext
    {
        public MechanicDb(DbContextOptions options) : base(options) { }
        public DbSet<Mechanic> Mechanics { get; set; }  
    }
}
