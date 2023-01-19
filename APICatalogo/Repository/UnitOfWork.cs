using APICatalogo.Context;

namespace APICatalogo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProdutoRepository _produtoRepository; //instanciar cada repositório e o dbcontext
        private CategoriaRepository _categoriaRepository;
        public AppDbContext _context; //instanciar o dbcontext

        public UnitOfWork(AppDbContext context)
        { 
            _context = context;//injeta no construtor
        }



        public IProdutoRepository ProdutoRepository //verifica se a instância de repositório é nula 
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);

            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }


      
        
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose(); //libera os recursos usados   
        }
    }
}
