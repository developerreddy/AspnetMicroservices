using Catalog.API.Entities;
using Catalog.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Repository.Classs
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateProduct(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                Product product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public async Task<Product> GetProduct(string id)
        {
            Product product = await _context.Products.FindAsync(id, "Category");
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            try
            {
                List<Product> productList = await _context.Products.Where(m => m.Category == categoryName).ToListAsync();
                return productList;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            List<Product> productList = await _context.Products.Where(m => m.Name == name).ToListAsync();
            return productList;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            List<Product> productList = await _context.Products.ToListAsync();
            return productList;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                Product prevProduct = await _context.Products.FindAsync(product.Id);
                _context.Products.Remove(product);
                _context.Products.Update(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
