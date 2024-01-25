using Microsoft.AspNetCore.Mvc;
using SmartPost.Api.Controllers.Commons;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.Categories;
using SmartPost.Service.Interfaces.Categories;

namespace SmartPost.Api.Controllers.Categories
{
    public class CategoriesController : BaseController
    {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CategoryForCreationDto dto)
           =>Ok(await _categoryService.CreateAsync(dto));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            =>Ok(await _categoryService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute (Name = "id")]long id)
            =>Ok(await _categoryService.RetrieveByIdAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name ="id")] long id, [FromBody] CategoryForUpdateDto dto)
            =>Ok(await _categoryService.ModifyAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute (Name = "id")]long id)
            =>Ok(await _categoryService.ReamoveAsync(id));
    }
}
