using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>(); //inicializar a propriedade produtos - boa prática - a responsabilidade da classe é inicializá-la.
    }

    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [MaxLength(80)]
    public string? Nome { get; set; }

    [Required]
    [MaxLength(300)]
    public string? ImagemUrl { get; set; }

    //[JsonIgnore]
    public ICollection<Produto>? Produtos { get; set; } //cada categoria pode ter uma coleção de produtos

}

  