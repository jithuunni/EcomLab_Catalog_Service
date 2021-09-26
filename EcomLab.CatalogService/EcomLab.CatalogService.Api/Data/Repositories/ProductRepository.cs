using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using EcomLab.CatalogService.Api.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EcomLab.CatalogService.Api.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("CatalogDbConnection"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("CollectionNames:Product"));
        }

        private IMongoCollection<Product> Products { get; set; }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            return await Products.Find(p => p.Category.Equals(category)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            return await Products.Find(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await Products.Find(p => p.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Product product)
        {
            await Products.InsertOneAsync(product);
        }

        public async Task InsertListAsync(IEnumerable<Product> products)
        {
            await Products.InsertManyAsync(products);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var result = await Products.ReplaceOneAsync(p => p.Id.Equals(product.Id), product);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await Products.DeleteOneAsync(p => p.Id.Equals(id));
            return result.DeletedCount > 0;
        }
    }// class ends
}
