using AutoMapper;
using ElectroShop.Data;
using ElectroShop.Dtos;
using ElectroShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetHierachyCategorie()
        {
            try
            {
                var listfromrepo = await _repo.GetHierachyCategorie();

                var toreturn = _mapper.Map<IEnumerable<CategorirsForTreeDto>>(listfromrepo);
                return Ok(toreturn);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet("linear")]
        public async Task<IActionResult> GetHierachyCategorie_dropdown()
        {
           var x= await _repo.GetHierachyCategorie_dropdown();
            return Ok(x);
        }

        [HttpPost("{Name}/and/{ParentId}")]
        public async Task<IActionResult> AddCategory([FromRoute]string Name, [FromRoute] int ParentId)
        {

            if (_repo.isCategotyTerminal(ParentId))
                return BadRequest("operation is not valid"); ;
            Category categoryob = new Category
            {
                Name = Name,
                ParentId = ParentId > 0 ? ParentId : (int?)null
            };
            _repo.Add<Category>(categoryob);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("failed to add Category");
        }
        #region property and Categoryproperty
        [HttpGet("GetProperties")]
        public async Task<IActionResult> GetProperties()
        {
            var Properties = await _repo.GetProperties();
            var PropertiesToReturn = _mapper.Map<IEnumerable<PropertiesForListDto>>(Properties);
            return Ok(PropertiesToReturn);
        }
        [HttpGet("GetPropertiesByCategory/{Id}")]
        public async Task<IActionResult> GetPropertiesByCategoryId([FromRoute]int Id)
        {
            try
            {
                var properties = await _repo.GetPropertiesByCategoryId(Id);
                var propertiesToreturn = _mapper.Map<IEnumerable<PropertiesForListDto>>(properties);
                return Ok(propertiesToreturn);
            }
            catch (Exception ex)
            {

                return BadRequest("");
            }
        }

        [HttpPost("AddProperty/{categoryId}")]
        public async Task<IActionResult> AddProperty([FromRoute]int categoryId, [FromBody]Property property)
        {
            //search kone ke category ba id categoryId vojod dashte bashe
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            CategoryProperty categoryPropertyob = new CategoryProperty
            {
                CategoryId = categoryId,
                RelatedProperty = property
            };
            //if ()
            //{
            //    PropertyOption propertyOption = new PropertyOption
            //    {
            //        Value = ""
            //        , Key = "" //??in chie?
            //        , RelatedProperty = property
            //    };
            //    _repo.Add<PropertyOption>(propertyOption);
            //}
            try
            {
                _repo.Add<Property>(property);
                _repo.Add<CategoryProperty>(categoryPropertyob);
                if (await _repo.SaveAll())
                    return Ok();
                return BadRequest("failed to add Property");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion
    }
    #region propertyOption

    #endregion

}
