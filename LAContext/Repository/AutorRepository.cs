

using LADomain;
using Microsoft.EntityFrameworkCore;

namespace LAContext
    {
    public class AutorRepository
        {
        public void Add(Autor autor)
            {
            using (var db = new LADbContext())
                {
                db.Autores.FromSqlRaw($"EXECUTE AddAutor @Id={autor.Id} @Nome={autor.Nome} @Sobrenome={autor.Sobrenome} " +
                    $"@Email={autor.Email} @Telefone={autor.Telefone} @Data={autor.Data} @ListaAutor={autor.Livros}").ToList();
                db.SaveChanges();
                }

            }

        public void Update(Autor autor)
            {
            using (var db = new LADbContext())
                {
                db.Autores.FromSqlRaw($"EXECUTE UpdateAutor @Id={autor.Id} @Nome={autor.Nome} @Sobrenome={autor.Sobrenome} " +
                    $"@Email={autor.Email} @Telefone={autor.Telefone} @Data={autor.Data} @ListaAutor={autor.Livros}").ToList();
                db.SaveChanges();
                }

            }

        public void Delete(int id)
            {
            using (var db = new LADbContext())
                {
                db.Autores.FromSqlRaw($"EXECUTE DeleteAutorById @Id={id}");
                db.SaveChanges();
                }

            }

        public Autor GetById(int id)
            {
            using (var db = new LADbContext())
                {
                var Autor = db.Autores.FromSqlRaw($"EXECUTE GetAutorById @Id={id}").ToList();
                if(Autor != null)
                    {
                    db.SaveChanges();
                    return Autor[0];
                    }
                return null;
                }
            

            }

        public IEnumerable<Autor> GetAll()
            {
            using (var db = new LADbContext())
                {
                var autores = db.Autores.FromSqlRaw("EXECUTE GetAllAutors").ToList();
                return (IEnumerable<Autor>)autores;
                }
            }
        }
    }
