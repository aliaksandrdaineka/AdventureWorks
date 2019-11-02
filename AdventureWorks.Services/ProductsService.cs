using AdventureWorks.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureWorks.Services
{
    public class ProductsService : IProductsService
    {
        public Product Create(Product product)
        {
            using (var db = new DataContext())
            {
                db.Product.Add(product);
                db.SaveChanges();

                return product;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (var db = new DataContext())
            {
                return db.Product.ToList();
            }
        }

        public Product GetById(int id)
        {
            using (var db = new DataContext())
            {
                return db.Product.Find(id);
            }
        }

        public void Remove(Product product)
        {
            using (var db = new DataContext())
            {
                db.Product.Remove(product);
                db.SaveChanges();
            }
        }

        public Product Update(Product product)
        {
            using (var db = new DataContext())
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return product;
            }
        }
    }
}
