using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uow;


        public ProdutosController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPrecos()
        {
            return _uow.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() //IEnumerable permite adiar a execução, trabalha por demanda e não precisa da coleção na memória
        {
            var produtos =_uow.ProdutoRepository.Get().ToList();

            //código de status de erro http
            if (produtos is null)
            {
                return NotFound();
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            //throw new Exception("Exception ao retonar o produto pelo id");
            
            
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            return produto;
        }

        [HttpPost]
        public ActionResult Produto([FromBody]Produto produto)
        {
            _uow.ProdutoRepository.Add(produto); //incluindo no contexto e trabalhando na memória
            _uow.Commit(); //persistência dos dados na tabela

            return new CreatedAtRouteResult("ObterProduto", //retorna um código de status 201 se criado, após adiciona o cabeçalho rotation na resposta.
                new {id = produto.ProdutoId}, produto); //cria um método 202 no head location
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto) //tem que inserir todos - a alternativa seria o path
        {
            if (id != produto.ProdutoId)
                return BadRequest();
            _uow.ProdutoRepository.Update(produto); //entidade precisa ser persistida;
            _uow.Commit();

            return Ok(produto);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            //var produto = _uow.Produtos.Find(id) //Alternativa que busca na memória e depois no banco, para isso o id precisa ser primary key
            if (produto is null)
                return NotFound("Produto não localizado...");
            _uow.ProdutoRepository.Delete(produto);
            _uow.Commit();

            return Ok(produto);

        }
    }
}
