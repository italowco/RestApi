using Microsoft.AspNetCore.Mvc;
using RestApi.Application.Controllers;
using RestApi.UnitTest.MockData;
using Moq;
using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestApi.UnitTest.System.Services;

namespace RestApi.UnitTest.System.Controllers
{
    public class TestProductController
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            /// Arrange
            var todoService = new Mock<ProductService>();
            todoService.Setup(_ => _.GetAllAsync()).ReturnsAsync(ProductMockData.GetProducts());
            var sut = new ProductController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await sut.GetAllAsync();


            // /// Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
