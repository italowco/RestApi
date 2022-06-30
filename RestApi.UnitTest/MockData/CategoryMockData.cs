using RestApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.UnitTest.MockData
{
    public class CategoryMockData
    {
        public static List<Category> GetCategories()
        {
            return new List<Category>{
                new Category{
                    Title = "Category 1",
                    id = 1,
                },
                new Category
                {
                    Title = "Category 2",
                    id = 2
                }
            };
        }
    }
}

