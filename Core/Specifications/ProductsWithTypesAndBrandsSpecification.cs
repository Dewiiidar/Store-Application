using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecifications<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams ProductParams)
            : base(x =>
            (string.IsNullOrEmpty(ProductParams.Search) || x.Name.ToLower().Contains(ProductParams.Search)) &&
            (!ProductParams.BrandId.HasValue || x.ProductBrandId == ProductParams.BrandId) &&
            (!ProductParams.TypeId.HasValue || x.ProductTypeId == ProductParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            ApplyPaging(ProductParams.PageSize * (ProductParams.PageIndex - 1), ProductParams.PageSize);
            if (!string.IsNullOrEmpty(ProductParams.sort))
            {
                switch (ProductParams.sort)
                {
                    case "PriceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int Id) : base(x => x.Id == Id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
