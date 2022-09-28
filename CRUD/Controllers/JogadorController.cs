using CRUD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogadorController : Controller
    {
        private readonly Campeonato_DbContext _DbContext;

        public JogadorController(Campeonato_DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _DbContext.Jogadors.ToListAsync());
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            return Ok(await _DbContext.Jogadors.Where(x => x.Cpf == cpf).FirstOrDefaultAsync());
        }

        [HttpPost]
        public IActionResult Create(Jogador jogador)
        {
            _DbContext.Jogadors.Add(jogador);
            _DbContext.SaveChanges();

            return Ok( new{ Message = "Jogador foi criado!" });
        }

        [HttpPut]
        public IActionResult Updated( Jogador jogador)
        {
            _DbContext.Jogadors.Update(jogador);
            _DbContext.SaveChanges();

            return Ok(new { Message = "Jogador foi alterado!" });
        }

        [HttpDelete]
        public IActionResult Deleted(Jogador jogador)
        {
            _DbContext.Jogadors.Remove(jogador);
            _DbContext.SaveChanges();

            return Ok(new { Message = "Jogador foi deletado" });

        }

    }
}
