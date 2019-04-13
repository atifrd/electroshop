using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ElectroShop.Data;
using ElectroShop.Dtos;
using ElectroShop.Models;

namespace ElectroShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repo.GetProducts();
            var productsToReturn = _mapper.Map<IEnumerable<ProductForListDto>>(products);
            return Ok(productsToReturn);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product
            //string OrginalName,string Title,bool IsPreview
            //,bool IsNew,bool IsEnabled,string Description,double SalesCount,int SellerId,int CategoryId,int BrandId
            )

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Product productob = new Product
            //{
            //    OrginalName = product.OrginalName,
            //    Title = product.Title,
            //    IsPreview = product.IsPreview,
            //    IsNew = product.IsNew,
            //    IsEnabled = product.IsEnabled,
            //    Description = product.Description,
            //    RegDate = System.DateTime.Now,
            //    SalesCount = product.SalesCount,
            //    SellerId = product.SellerId,
            //    CategoryId = product.CategoryId,
            //    BrandId = product.BrandId
            //};
            _repo.Add<Product>(product);
            //foreach (var item in product.Images)
            //{
            //    item.RelatedProduct = product;
            //    _repo.Add<ImageGallery>(item);
            //}
            //foreach (var item in product.ProductProperties)
            //{
            //    item.RelatedProduct = product;
            //    _repo.Add<ProductProperty>(item);
            //}


            try
            {
                if (await _repo.SaveAll())
                    return Ok(product.Id);

            }
            catch (Exception ex)
            {

                throw;
            }
            
            return BadRequest("failed to add Product");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody]ProductForUpdateDto productForUpdateDto)
        {
            var productFromRepo = await _repo.GetProductById(id);
            if (productFromRepo == null) return NotFound($"could not find product with an ID of {id}");
            _mapper.Map(productForUpdateDto, productFromRepo);
            if (await _repo.SaveAll())
                return NoContent();
            throw new Exception("update failed");
            //return BadRequest("failed to like user");?? farghe in do ta
        }

        [HttpGet("GetProductsByCategory/{CategoryId}")]
        public async Task<IActionResult> GetProductsByCategory([FromRoute]int CategoryId)
        {
            var productFromRepo = await _repo.GetProductsByCategory(CategoryId);
            var productsToReturn = _mapper.Map<IEnumerable<ProductForListDto>>(productFromRepo);
            return Ok(productsToReturn);

        }

    }

}
