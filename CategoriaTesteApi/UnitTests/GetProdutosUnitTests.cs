using CategoriaAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CategoriaTesteApi.UnitTests
{
    public class GetProdutosUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutoController _controller;

        public GetProdutosUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutoController(controller.repository);
        }

        [Fact]
        public void GetOk()
        {
            //Arrange
            string produto = "notebook";

            //Act
            var data = _controller.Get(produto, null);

            //Assert
            data.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetNotFound()
        {
            //Arrange
            string produto = "Abacate";

            //Act
            var data = _controller.Get(produto, null);

            //Assert
            data.Result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public void GetByIdOk()
        {
            //Arrange
            int codigoProduto = 2;

            //Act
            var data = _controller.Detail(codigoProduto);

            //Assert
            data.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetByIdNotFound()
        {
            // Arrange
            int codigoProduto = 50;

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _controller.Detail(codigoProduto));

            // Verifica a mensagem da exceção
            Assert.Equal("Produto não encontrado", exception.Message);
        }

    }
}
