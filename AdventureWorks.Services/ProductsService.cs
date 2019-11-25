using AdventureWorks.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureWorks.Services
{
    public class ProductsService : IProductsService
    {
        private readonly DataContext _context;

        public ProductsService(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {

            return await _context.Product.FindAsync(id);
        }

        public async Task Remove(Product item)
        {
            var product = await _context.Product.FindAsync(item.ProductId);
            if (product == null) return;

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
