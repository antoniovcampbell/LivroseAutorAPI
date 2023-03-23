
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
    public class AutoresController : ControllerBase
    {
        private readonly AutorService _context;

        public AutoresController(AutorService context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            var autores = _context.ObterAutores().ToList();

            if (autores == null)
            {
                return NotFound();
            }

            return autores;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = _context.ObterAutorPorId(id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.AtualizarAutor(autor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))

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
        public async Task<ActionResult<Autor>> PostAutor(Autor autor)
        {
            if (autor == null)
            {
                return BadRequest();
            }
            var amigoantigo = _context.ObterAutorPorId(autor.Id);
            if(amigoantigo != null)
            {
                return BadRequest();
            }
            _context.AdicionarAutor(autor);
            
            return CreatedAtAction("GetAutor", new { id = autor.Id }, autor);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            if (_context.ObterAutores() == null)
            {
                return NotFound();
            }
            var amigo = _context.ObterAutorPorId(id);
            if (amigo == null)
            {
                return NotFound();
            }

            _context.ExcluirAutor(amigo);
            

            return NoContent();
        }

        private bool AutorExists(int id)
        {
            var amigo = _context.ObterAutorPorId(id);
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
