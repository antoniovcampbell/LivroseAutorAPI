
using LADomain;
using Microsoft.EntityFrameworkCore;

namespace LAContext
{
    public class LADbContext : DbContext
    {
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        public LADbContext()
        {
            Database.EnsureCreated();
        }
        public LADbContext(DbContextOptions<LADbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Initial Catalog=LADatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).ValueGeneratedOnAdd();
                x.Property(x => x.Nome).HasMaxLength(100).IsRequired();
                x.Property(x => x.Sobrenome).HasMaxLength(200);
                x.Property(x => x.Email).HasMaxLength(200).IsRequired();
                x.Property(x => x.Telefone);
                x.Property(x => x.Data).IsRequired();
                x.Ignore(x => x.Livros);
            });
            modelBuilder.Entity<Livro>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).ValueGeneratedOnAdd();
                x.Property(x => x.Titulo).HasMaxLength(100);
                x.Property(x => x.ISBN).IsRequired();
                x.Property(x => x.Ano).IsRequired();
                x.Ignore(x => x.Autores);
            });
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LADbContext).Assembly);
        }
       
    }
}
