using Microsoft.AspNetCore.Mvc;
using OscarFilmeApi.Data;
using OscarFilmeApi.Models;

namespace OscarFilmeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilmesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Filme filme)
        {
            if (filme.AnoLancamento < 1929)
                return BadRequest("O Oscar começou em 1929!");

            _context.Filmes.Add(filme);
            _context.SaveChanges();

            return Ok(filme);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Filmes.ToList());
        }

        [HttpGet("vencedores")]
        public IActionResult GetVencedores()
        {
            return Ok(_context.Filmes.Where(f => f.Venceu).ToList());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Filme atualizado)
        {
            var filme = _context.Filmes.Find(id);

            if (filme == null)
                return NotFound();

            filme.Titulo = atualizado.Titulo;
            filme.Diretor = atualizado.Diretor;
            filme.Categoria = atualizado.Categoria;
            filme.AnoLancamento = atualizado.AnoLancamento;
            filme.Venceu = atualizado.Venceu;

            if (filme.Venceu)
                Console.WriteLine($"Temos um novo vencedor: {filme.Titulo}!");

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var filme = _context.Filmes.Find(id);

            if (filme == null)
                return NotFound();

            _context.Filmes.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("estatisticas")]
        public IActionResult Estatisticas()
        {
            var total = _context.Filmes.Count();
            var vencedores = _context.Filmes.Count(f => f.Venceu);

            return Ok(new { total, vencedores });
        }
    }
}
