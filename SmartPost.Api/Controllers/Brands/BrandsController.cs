using Microsoft.AspNetCore.Mvc;
using SmartPost.Api.Controllers.Commons;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.Brands;
using SmartPost.Service.Interfaces.Brands;

namespace SmartPost.Api.Controllers.Brands
{
    public class BrandsController : BaseController
    {
        IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BrandForCreationDto dto)
           => Ok(await _brandService.CreateAsync(dto));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _brandService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
            => Ok(await _brandService.RetrieveByIdAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id, [FromBody] BrandForUpdateDto dto)
            => Ok(await _brandService.ModifyAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
            => Ok(await _brandService.ReamoveAsync(id));
    }

}
