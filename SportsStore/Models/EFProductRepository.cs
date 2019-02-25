using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFProductRepository: IProductRepository
    {
        private ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products => _context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
                _context.Products.Add(product);
            else
            {
                Product productDb = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                if(productDb != null)
                {
                    productDb.Name = product.Name;
                    productDb.Description = product.Description;
                    productDb.Category = product.Category;
                    productDb.Price = product.Price;
                }
            }
            _context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == productId);

            if(product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return product;
        }
    }
}
