using Microsoft.EntityFrameworkCore;
using BurnBook.Models.Entities;

namespace BurnBook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<BurnEntry>
            BurnEntries
        { get; set; }
    }
}