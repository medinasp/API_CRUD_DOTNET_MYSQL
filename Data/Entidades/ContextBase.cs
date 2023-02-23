using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Entidades
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        public ContextBase(DbContextOptions<ContextBase> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_configuration.GetConnectionString("DefaultConnection"),
                                                    ServerVersion.AutoDetect(_configuration.GetConnectionString("DefaultConnection")));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }
    }
}