using Microsoft.EntityFrameworkCore;
using RestApi.Domain.Model;
using RestApi.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.UnitTest.System.Services
{
    public class ProductService : IDisposable
    {

        private readonly DataContext _context;

        public ProductService()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new DataContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllAsync_ReturnTodoCollection()
        {
            /// Arrange
            _context.Products.AddRange(MockData.ProductMockData.GetProducts());
            _context.SaveChanges();

            var sut = new ProductService(_context);

            /// Act
            var result = await sut.GetAllAsync();

            /// Assert
            result.Should().HaveCount(TodoMockData.GetTodos().Count);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
