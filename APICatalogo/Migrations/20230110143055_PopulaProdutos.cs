using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class PopulaProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)"+
                                 " Values('Coca-Cola Diet', 'Refrigerante de cola', 5.45, 'cocacola.jpg', 50, now(), 1)");
            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                                 " Values('Pepsi', 'Refrigerante de cola', 4.80, 'pepsi.jpg', 50, now(), 1)");
            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                                 " Values('Coxinha', 'Salgado de frango', 3.00, 'coxinha.jpg', 50, now(), 2)");
            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                                 " Values('Brigadeiro', 'Doce de chocolate', 1.00, 'brigadeiro.jpg', 50, now(), 3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Produtos");
        }
    }
}
