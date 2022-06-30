using RestApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.UnitTest.MockData
{
    public class ProductMockData
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>{
                new Product{
                    CategoryId = 1,
                    Description = "Produto 1",
                    Id = 1,
                    Price = 05,
                    Title = "Produto 1",
                },
                new Product{
                    CategoryId = 2,
                    Description = "Produto 2",
                    Id = 1,
                    Price = 05,
                    Title = "Produto 2",
                },
                new Product{
                    CategoryId = 2,
                    Description = "Produto 3",
                    Id = 1,
                    Price = 05,
                    Title = "Produto 3",
                },
            };
        }
    }
}
