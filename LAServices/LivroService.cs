

using LAContext;
using LADomain;


namespace LAService
{
    public class LivroService
    {
        private LivroRepository context;

        public LivroService(LivroRepository _context)
        {
            this.context = _context;
        }

        public IEnumerable<Livro> ObterLivros()
        {
            
            var Livros = context.GetAll();
            if (Livros == null)
                throw new BusinessException("Lista de Livros Vazia");
            return Livros;
        }


        public Livro ObterLivroPorId(int id)
        {
            var Livro = context.GetById(id);
            if (Livro == null)
                throw new BusinessException("Este Livro não existe");

            return Livro;

        }

        public Livro AdicionarLivro(Livro Livro)
        {
 
            if (Livro != null)
                throw new BusinessException("Livro já cadastrado");
            try
            {
                this.context.Add(Livro);
            }
            catch(BusinessException e)
            {
                e.title = "Livro não foi adicionado";
            }
            return Livro;
        }

        public Livro AtualizarLivro(Livro LivroNovo)
        {
            try
            {
                this.context.Update(LivroNovo);
               
            }
            catch (BusinessException ex)
            {
                ex.title = "Livro não foi atualizado";
            }
            

            return LivroNovo;

        }

        public void ExcluirLivro(Livro Livro)
        {
            try
            {
                this.context.Delete(Livro.Id);
                
            }catch (BusinessException ex)
            {
                ex.title = "Livro não foi removido";
            }
           
        }

        public Livro AutenticarLivro(int Id , int isbn)
        {
            var Livro = this.ObterLivros().FirstOrDefault(
                    x => x.Id == Id
                );

            return Livro;

        }


    }
}
