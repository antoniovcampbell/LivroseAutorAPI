

using LADomain;
using Microsoft.EntityFrameworkCore;


namespace LAContext
    {
    public class LivroRepository
        {
        public void Add(Livro Livro)
            {
            using (var db = new LADbContext())
                {
                db.Livros.FromSqlRaw($"EXECUTE AddLivro Id={Livro.Id} @Titulo={Livro.Titulo} @ISBN={Livro.ISBN} @Ano={Livro.Ano} @Autores={Livro.Autores}").ToList();
                db.SaveChanges();
                }

            }

        public void Update(Livro Livro)
            {
            using (var db = new LADbContext())
                {
                db.Livros.FromSqlRaw($"EXECUTE UpdateLivro Id={Livro.Id} @Titulo={Livro.Titulo} @ISBN={Livro.ISBN} @Ano={Livro.Ano} @Autores={Livro.Autores}").ToList();
                db.SaveChanges();
                }

            }

        public void Delete(int id)
            {
            using (var db = new LADbContext())
                {
                db.Livros.FromSqlRaw($"EXECUTE DeleteLivroById @Id = {id}");
                db.SaveChanges();
                }

            }

        public Livro GetById(int id)
            {
            using (var db = new LADbContext())
                {
                var Livro = db.Livros.FromSqlRaw($"EXECUTE GetLivroById @Id = {id}").ToList();
                if(Livro != null)
                    {
                    db.SaveChanges();
                    return Livro[0];
                    }
                return null;
                }
            

            }

        public IEnumerable<Livro> GetAll()
            {
            using (var db = new LADbContext())
                {
                var livros = db.Livros.FromSqlRaw("EXECUTE GetAllLivros").ToList();
                return (IEnumerable<Livro>)livros;
                }
            }
        }
    }
