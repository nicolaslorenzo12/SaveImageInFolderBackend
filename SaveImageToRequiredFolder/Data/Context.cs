using Microsoft.EntityFrameworkCore;
using SaveImageToRequiredFolder.Models;
namespace SaveImageToRequiredFolder.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Image> Images { get; set; }    
    }
}
