using AdminEntity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace AdminEntity.Infrastructure
{
    public class ADMIN_MODELS : DbContext
    {

        //public ADMIN_MODELS(DbContextOptions<ADMIN_MODELS> option)
        //: base(option)
        //{
        //    Database.EnsureCreated();
        //}
        public ADMIN_MODELS(string connectionString) : base(GetOptions(connectionString))
        {
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return MySqlDbContextOptionsExtensions.UseMySql(new DbContextOptionsBuilder(), connectionString).Options;
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          //  IConfiguration _config= conf
            if (!optionsBuilder.IsConfigured)
            {
           //     _config.GetValue
               // string value = System.Configuration.ConfigurationManager.AppSettings[key];
                string ConnectionStrings = ConfigurationManager.AppSettings["ConnectionStrings:Default"];
                optionsBuilder.UseMySql(ConnectionStrings);
                //    optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=EMP_CRUD");
            }
        }

        public DbSet<USERS> USERS { get; set; }
        public DbSet<EMPLOYEES> EMPLOYEES { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<USERS>().ToTable("USERS");
            modelBuilder.Entity<EMPLOYEES>().ToTable("EMPLOYEES");
        }
    }
}
