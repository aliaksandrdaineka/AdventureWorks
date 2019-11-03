using AdventureWorks.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Services
{
    public class ProductsService : IProductsService
    {
        public async Task<Product> Create(Product product)
        {
            using (var db = new DataContext())
            {
                await db.Product.AddAsync(product);
                await db.SaveChangesAsync();

                return product;
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using (var db = new DataContext())
            {
                return await db.Product.ToListAsync();
            }
        }

        public async Task<Product> GetById(int id)
        {
            using (var db = new DataContext())
            {
                return await db.Product.FindAsync(id);
            }
        }

        public async Task Remove(Product item)
        {
            using (var db = new DataContext())
            {
                var product = await db.Product.FindAsync(item.ProductId);
                if (product == null) return;
                db.Product.Remove(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Product> Update(Product product)
        {
            using (var db = new DataContext())
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return product;
            }
        }
    }
}
