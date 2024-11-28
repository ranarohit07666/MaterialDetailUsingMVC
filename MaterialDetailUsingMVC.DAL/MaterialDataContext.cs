using MaterialDetailUsingMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaterialDetailUsingMVC.DLL
{
    public class MaterialDataContext : IdentityDbContext<User>
    {
        public MaterialDataContext(DbContextOptions<MaterialDataContext> options) : base(options)
        {

        }
        public DbSet<User> tbl_user { get; set; }
        public DbSet<ReferenceDetail> tbl_referencedetail { get; set; }
        public DbSet<Material> tbl_material { get; set; }
        public DbSet<Unit> tbl_unit { get; set; }
        public DbSet<Types> tbl_type { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReferenceDetail>()
                .Property(m => m.ReferenceNumber)
                .HasComputedColumnSql("'REF-' + FORMAT([Id], '0000')");

            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "NOS" },
                new Unit { Id = 2, Name = "PKT" },
                new Unit { Id = 3, Name = "BOX" },
                new Unit { Id = 4, Name = "ITM" },
                new Unit { Id = 5, Name = "ROL" },
                new Unit { Id = 6, Name = "LTR" }
            );
            
            modelBuilder.Entity<Types>().HasData(
                new Types { Id = 1, Name = "Acc0185" },
                new Types { Id = 2, Name = "Acc0567" },
                new Types { Id = 3, Name = "Dev0476" },
                new Types { Id = 4, Name = "ADM6633" }
            );

        }
    }
}
