using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification :BaseSpecifications<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams ProductParams) : base(x =>
             (string.IsNullOrEmpty(ProductParams.Search) || x.Name.ToLower().Contains(ProductParams.Search)) &&
             (!ProductParams.BrandId.HasValue || x.ProductBrandId == ProductParams.BrandId) &&
             (!ProductParams.TypeId.HasValue || x.ProductTypeId == ProductParams.TypeId)
            )
        {

        }
    }
}
