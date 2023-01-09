using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>(); //inicializar a propriedade produtos - boa prática - a responsabilidade da classe é inicializá-la.
    }
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }

    public ICollection<Produto>? Produtos { get; set; } //cada categoria pode ter uma coleção de produtos

}

