

using LAContext;
using LADomain;
using System.Security.Cryptography.X509Certificates;

namespace LAService
{
    public class AutorService
    {
        private readonly AutorRepository _context;

        public AutorService(AutorRepository context)
        {
            _context = context;
        }

        public IEnumerable<Autor> ObterAutores()
        {
            
            var Autores = _context.GetAll().ToList();
            if (Autores == null)
                throw new BusinessException("Lista de Autors Vazia");
            return Autores;
        }


        public Autor ObterAutorPorId(int id)
        {
            var Autor = _context.GetById(id);
            if (Autor == null)
                throw new BusinessException("Este Autor não existe");

            return Autor;

        }

        public Autor AdicionarAutor(Autor Autor)
        {
            

            if (Autor != null)
                throw new BusinessException("Autor Vazio");

            try
            {
                _context.Add(Autor);
            }catch(BusinessException e)
            {
                e.title = "Autor não foi adicionado";
            }
            
            

            return Autor;
        }

        public Autor AtualizarAutor(Autor AutorNovo)
        {

            try
            {

                _context.Update(AutorNovo);
                
            }
            catch (BusinessException ex)
            {
                ex.title = "Autor não foi atualizado";
            }
            

            return AutorNovo;

        }

        public void ExcluirAutor(Autor Autor)
        {
            try
            {
                _context.Delete(Autor.Id);
                
            }catch (BusinessException ex)
            {
                ex.title = "Autor não foi removido";
            }
           
        }

        public Autor AutenticarAutor(string email)
        {
            var Autor = this.ObterAutores().FirstOrDefault(
                    x => x.Email == email
                );

            return Autor;

        }


    }
}
