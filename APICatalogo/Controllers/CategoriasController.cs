using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository;
using APICatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        
        public CategoriasController(IUnitOfWork uow, IConfiguration config, ILogger<CategoriasController> logger)
        {
            _uow = uow;
            _configuration = config;
            _logger = logger;
        } 

        [HttpGet("autor")]
        public string GetAutor()
        {
            var autor = _configuration["autor"];
            return $"Autor: {autor}";
        }



        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuServico, string nome)
        {
            _logger.LogInformation("=============== GET api/categorias/saudacao ==================");
            return meuServico.Saudacao(nome);
        }
        
        
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            _logger.LogInformation("=============== GET api/categorias/ ==================");
            //return _uow.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();
            try
            {
                return _uow.CategoriaRepository.GetCategoriasProdutos().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _uow.CategoriaRepository.Get().ToList();
                if (categorias is null)
                    return NotFound();
                return categorias;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");

            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
                if (categoria is null)
                    return NotFound($"Categoria com id = {id} não encontrada");
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }

        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                    return BadRequest("Categoria vazia");
                _uow.CategoriaRepository.Add(categoria);
                _uow.Commit();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação.");
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest("Dados Inválidos");
                }

                _uow.CategoriaRepository.Update(categoria);
                _uow.Commit();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação.");
            }

        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
                if (categoria == null)
                    return NotFound($"Categoria com id={id} não encontrada");

                _uow.CategoriaRepository.Delete(categoria);
                _uow.Commit();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação.");
            }


        }
    }
}
