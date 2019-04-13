using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ElectroShop.Data;
using ElectroShop.Dtos;
using ElectroShop.Models;
using Microsoft.AspNetCore.Mvc;


namespace ElectroShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPropertyController:Controller
    {
        private readonly IProductPropertyRepository _repo;
        private readonly IMapper _mapper;
        public ProductPropertyController(IProductPropertyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("ProdProp_toInsert/{id}/{productId}")]
        public async Task<IActionResult> GetProductProperties_toInsert([FromRoute] int id,[FromRoute] long productId)
        {
            var Toreturn= await _repo.GetProductProperties_toInsert(id, productId);
            return  Ok(Toreturn);
        }

        [HttpPost("AddProProp")]
        public async Task<IActionResult> AddProductProperty([FromBody] ProductPropertyToInsert[] productPropertyPram)
        {
            foreach (var item in productPropertyPram)
            {
                ProductProperty productPropertyob = new ProductProperty
                {
                    ProductId = item.ProductId,
                    PropertyId = item.propertyId,
                    PropertyValue = item.PropertyValue
                };
                _repo.Add<ProductProperty>(productPropertyob);
            }
            
            try
            {  
                if (await _repo.SaveAll())
                    return Ok();
                return BadRequest("failed to add productProperty");

            }
            catch (Exception ex)
            {

                return BadRequest("failed to add productProperty");
            }
            
        }
    }
}
