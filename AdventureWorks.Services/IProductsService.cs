using AdventureWorks.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureWorks.Services
{
    public interface IProductsService
    {
        Product GetById(int id);
        Product Create(Product product);
        Product Update(Product product);

        void Remove(Product product);

        IEnumerable<Product> GetAll();
    }
}
