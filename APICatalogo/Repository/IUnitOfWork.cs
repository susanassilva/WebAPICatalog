namespace APICatalogo.Repository
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; } //obtém o repositório de produto
        ICategoriaRepository CategoriaRepository { get; }

        void Commit();
    }
}
