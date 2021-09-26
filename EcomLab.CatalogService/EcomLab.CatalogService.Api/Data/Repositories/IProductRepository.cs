using System.Collections.Generic;
using System.Threading.Tasks;
using EcomLab.CatalogService.Api.Data.Entities;

namespace EcomLab.CatalogService.Api.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<IEnumerable<Product>> GetByCategoryAsync(string category);

        Task<IEnumerable<Product>> GetByNameAsync(string name);

        Task<Product> GetByIdAsync(string id);

        Task InsertAsync(Product product);

        Task InsertListAsync(IEnumerable<Product> products);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(string id);
    }
}
