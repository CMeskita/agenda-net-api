using AgendaNet_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Principal;

namespace AgendaNet_Infra.Context
{
    public class PostgreContext : DbContext
    {
        public PostgreContext(DbContextOptions<PostgreContext> options):base(options){ }
        #region dbset
        //Define seus DbSets aqui
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<EstablishmentTenant> EstablishmentTenants { get; set; }
        public DbSet<Document> Documents { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Establishment>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Contact>().HasKey(x => x.Id);
            modelBuilder.Entity<Document>().HasKey(x => x.Id);
            modelBuilder.Entity<EstablishmentTenant>().HasKey(x => x.EstablishmentId);

            modelBuilder.Entity<EstablishmentTenant>()
                .Property(b => b.AtributosEstablisment)
                .HasColumnType("jsonb");
        }
    }
}
