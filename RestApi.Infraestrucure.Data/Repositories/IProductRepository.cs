using RestApi.Domain.Model;
using RestApi.Infraestructure.Repositories.Interfaces;

namespace RestApi.Infraestructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}