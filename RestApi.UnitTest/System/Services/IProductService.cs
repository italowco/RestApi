using RestApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.UnitTest.System.Services
{
    public interface IDisposable1
    {
        public Task<List<Product>> GetProducts();
        public Task SaveAsync(Product newProduct);
    }
}
