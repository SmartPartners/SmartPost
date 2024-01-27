using Microsoft.AspNetCore.Mvc;
using SmartPost.Service.DTOs.Products;
using SmartPost.Service.Interfaces.Products;
using System.ComponentModel.DataAnnotations;

namespace SmartPost.Api.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult>GetAllAsync()
            =>Ok(await _productService.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([Required] long id) 
            =>Ok(await _productService.GetByIdAsync(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync([Required] long  id)
            =>Ok(await _productService.DeleteAsync(id));

        [HttpPut]
        public async Task<IActionResult> ModifyAsync(long id,[FromBody]ProductForUpdateDto productForUpdate)
            =>Ok(await _productService.UpdateAsync(id,productForUpdate));

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ProductForCreationDto productForCreationDto)
            => Ok(await _productService.CreateAsync(productForCreationDto));
    }
}
