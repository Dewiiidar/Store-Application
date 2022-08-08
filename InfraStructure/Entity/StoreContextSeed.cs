using Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfraStructure.Entity
{
    public class StoreContextSeed
    {
        public static async Task SeedAsynk(StoreContext context)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var BrandData = File.ReadAllText("../InfraStructure/Entity/SeedData/brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                    foreach (var item in Brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var TypeData = File.ReadAllText("../InfraStructure/Entity/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                    foreach (var item in Types)
                    {
                        context.ProductTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Product.Any())
                {
                    var ProductData = File.ReadAllText("../InfraStructure/Entity/SeedData/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    foreach (var item in Products)
                    {
                        context.Product.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
            }
        }
    }
}
