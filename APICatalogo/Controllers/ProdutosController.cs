using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;


        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos() //IEnumerable permite adiar a execução, trabalha por demanda e não precisa da coleção na memória
        {
            var produtos = _context.Produtos.AsNoTracking().ToList();

            //código de status de erro http
            if (produtos is null)
            {
                return NotFound();
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            return produto;
        }

        [HttpPost]
        public ActionResult Produto(Produto produto)
        {
            _context.Produtos.Add(produto); //incluindo no contexto e trabalhando na memória
            _context.SaveChanges(); //persistência dos dados na tabela

            return new CreatedAtRouteResult("ObterProduto", //retorna um código de status 201 se criado, após adiciona o cabeçalho rotation na resposta.
                new {id = produto.ProdutoId}, produto); //cria um método 202 no head location
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto) //tem que inserir todos - a alternativa seria o path
        {
            if (id != produto.ProdutoId)
                return BadRequest();
            _context.Entry(produto).State = EntityState.Modified; //entidade precisa ser persistida;
            _context.SaveChanges();

            return Ok(produto);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            //var produto = _context.Produtos.Find(id) //Alternativa que busca na memória e depois no banco, para isso o id precisa ser primary key
            if (produto is null)
                return NotFound("Produto não localizado...");
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);

        }
    }
}
