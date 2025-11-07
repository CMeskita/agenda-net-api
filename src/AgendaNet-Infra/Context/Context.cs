using AgendaNet_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaNet_Infra.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options):base(options){ }
        #region dbset
        //Define seus DbSets aqui
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Establishment> Establishments { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configurações adicionais de mapeamento podem ser feitas aqui
        }
    }
}
