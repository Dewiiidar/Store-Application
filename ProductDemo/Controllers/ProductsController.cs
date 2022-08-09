using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Entity;
using Core.Models;
using Core.Interfaces;
using Core.Specifications;
using ProductDemo.DTOs;
using ProductDemo.Helpers;
using AutoMapper;
using ProductDemo.Errors;

namespace ProductDemo.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo, IGenericRepository<ProductBrand> ProductBrandRepo, IGenericRepository<ProductType> ProductTypeRepo, IMapper mapper)
        {
            _productRepo = ProductRepo;
            _productBrandRepo = ProductBrandRepo;
            _productTypeRepo = ProductTypeRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery]ProductSpecParams Params)
        {
            var Spec = new ProductsWithTypesAndBrandsSpecification(Params);
            var Count = new ProductWithFiltersForCountSpecification(Params);
            var totalItems = await _productRepo.CountAsync(Count);
             var products = await _productRepo.ListAsync(Spec);
            var data = Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
            return Ok(new Pagination<ProductToReturnDTO>(Params.PageIndex,Params.PageSize,totalItems,data));

        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var Spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(Spec);

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Mapper.Map<Product, ProductToReturnDTO>(product);

        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}
