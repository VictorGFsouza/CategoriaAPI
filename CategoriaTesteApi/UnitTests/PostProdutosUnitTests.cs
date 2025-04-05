using CategoriaAPI.Controllers;
using CategoriaAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CategoriaTesteApi.UnitTests
{
    public class PostProdutosUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutoController _controller;

        public PostProdutosUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutoController(controller.repository);
        }

        [Fact]
        public void PostOk()
        {
            //Arrange
            var novoProduto = new Produto
            {
                CodigoCategoria = 1,
                Nome = "Mouse Logitech",
                Valor = 100,
                Descricao = "Mouse sem fio da Logitech"
            };

            //Act
            var data = _controller.Post(novoProduto);

            //Assert
            var createdResult = data.Result.Should().BeOfType<OkObjectResult>();
            createdResult.Subject.StatusCode.Should().Be(200);
        }
    }
}
