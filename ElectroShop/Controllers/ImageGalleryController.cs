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

    public class ImageGalleryController : Controller
    {
        private readonly IImageGalleryRepository _repo;
        private readonly IMapper _mapper;
        public ImageGalleryController(IImageGalleryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> AddImage([FromBody] ImageGallery imageGallery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _repo.Add(imageGallery);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("failed to add Image");
        }
        [HttpGet("imgproduct/{pid}")]
        public async Task<ActionResult> GetImagesByProduct([FromRoute]long pid)
        {
          var x=await  _repo.GetImageByRelatedProductId(pid);
            return Ok(x);
        }

    }
}
