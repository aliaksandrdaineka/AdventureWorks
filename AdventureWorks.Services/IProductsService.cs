using AdventureWorks.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Services
{
    public interface IProductsService
    {
        Task<Product> GetById(int id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);

        Task Remove(Product product);

        Task<IEnumerable<Product>> GetAll();
    }
}
