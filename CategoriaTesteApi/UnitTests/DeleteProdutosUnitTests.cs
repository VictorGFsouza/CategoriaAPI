using CategoriaAPI.Controllers;
using CategoriaAPI.Models;
using CategoriaAPI.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CategoriaTesteApi.UnitTests
{
    public class DeleteProdutosUnitTests
    {
        private readonly Mock<IProdutoRepository> _repositoryMock;
        private readonly ProdutoController _controller;

        public DeleteProdutosUnitTests()
        {
            _repositoryMock = new Mock<IProdutoRepository>();
            _controller = new ProdutoController(_repositoryMock.Object);
        }

        [Fact]
        public void DeleteOK()
        {
            // Arrange
            int codigoProduto = 3;
            var produto = new Produto { Codigo = codigoProduto, Nome = "Produto Teste", Valor = 99, CodigoCategoria = 1 };

            // Simula que Detail retorna um produto existente
            _repositoryMock.Setup(r => r.Detail(codigoProduto)).Returns(produto);

            // Act
            var result = _controller.Delete(codigoProduto);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            // Verifica se o método Delete foi chamado exatamente uma vez
            _repositoryMock.Verify(r => r.Delete(codigoProduto), Times.Once);
        }


        [Fact]
        public void DeleteNotFound()
        {
            // Arrange
            int codigoProduto = 8;
            _repositoryMock.Setup(r => r.Detail(codigoProduto)).Returns((Produto)null);

            // Act
            var result = _controller.Delete(codigoProduto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _repositoryMock.Verify(r => r.Delete(codigoProduto), Times.Never);
        }
    }
}
