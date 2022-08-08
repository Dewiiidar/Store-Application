using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Entity
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductByIdAsynk(int id)
        {
            var _element =  await _context.Product.Include(p => p.ProductBrand).Include(e => e.ProductType).FirstOrDefaultAsync(i=>i.Id == id);
            return _element;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsynk()
        {
            return await _context.Product.Include(p=>p.ProductBrand).Include(e=>e.ProductType).ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsynk()
        {
            return await _context.ProductTypes.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsynk()
        {
            return await _context.ProductBrands.ToListAsync();
        }
    }
}
