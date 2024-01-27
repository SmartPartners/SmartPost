using Microsoft.AspNetCore.Mvc;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.StockProducts;
using SmartPost.Service.Interfaces.StockProducts;

namespace SmartPost.Api.Controllers.StockProducts
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockProductController : ControllerBase
    {
        private readonly IStockProductService _stockProductService;

        public StockProductController(IStockProductService stockProductService)
        {
            _stockProductService = stockProductService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _stockProductService.GetByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GeAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _stockProductService.GetAllAsync(@params));

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] StockProductForCreationDto stockProductForCreation)
            => Ok(await _stockProductService.CreateAsync(stockProductForCreation));

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyAsync(long id, [FromBody] StockProductForUpdateDto stockProductForUpdate)
            => Ok(await _stockProductService.UpdateAsync(id, stockProductForUpdate));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _stockProductService.DeleteAsymc(id));


    }
}
