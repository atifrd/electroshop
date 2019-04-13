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
    public class BrandController:Controller
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandController(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpPost("{name}")]
        public async Task<ActionResult> AddBrand([FromRoute] string name)
        {
            Brand brandob = new Brand
            {
                IsEnabled = true,
                Name = name
            };

            _repo.Add(brandob);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("failed to add brand");


        }
        [HttpGet]
        public async Task<ActionResult> GetBrands()
        {
          var brands=  await _repo.GetBrands();
            return Ok(brands);
        }

    }
}
