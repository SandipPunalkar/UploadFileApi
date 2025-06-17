using Microsoft.EntityFrameworkCore;
using UploadFileApi.Entities;

namespace UploadFileApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<ImageEntity> Images { get; set; }

    }
}
