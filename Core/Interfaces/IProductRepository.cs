using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsynk(int id);
        Task<IReadOnlyList<Product>> GetProductsAsynk();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsynk();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsynk();

    }
}
