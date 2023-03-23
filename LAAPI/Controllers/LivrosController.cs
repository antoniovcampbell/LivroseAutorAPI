
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LAService;
using LADomain;

namespace AmigosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LivrosController : ControllerBase
    {
        private readonly LivroService _context;

        public LivrosController(LivroService context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            var Livros = _context.ObterLivros().ToList();

            if (Livros == null)
            {
                return NotFound();
            }

            return Livros;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var Livro = _context.ObterLivroPorId(id);

            if (Livro == null)
            {
                return NotFound();
            }

            return Livro;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro Livro)
        {
            if (id != Livro.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.AtualizarLivro(Livro);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))

                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro Livro)
        {
            if (Livro == null)
            {
                return BadRequest();
            }
            var amigoantigo = _context.ObterLivroPorId(Livro.Id);
            if(amigoantigo != null)
            {
                return BadRequest();
            }
            _context.AdicionarLivro(Livro);
            
            return CreatedAtAction("GetLivro", new { id = Livro.Id }, Livro);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            if (_context.ObterLivros() == null)
            {
                return NotFound();
            }
            var amigo = _context.ObterLivroPorId(id);
            if (amigo == null)
            {
                return NotFound();
            }

            _context.ExcluirLivro(amigo);
            

            return NoContent();
        }

        private bool LivroExists(int id)
        {
            var amigo = _context.ObterLivroPorId(id);
            if(amigo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        
    }
}
