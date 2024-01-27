using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GeAllAsync()
            => Ok(await _stockProductService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult>AddAsync([FromBody] StockProductForCreationDto stockProductForCreation)
            =>Ok(await _stockProductService.CreateAsync(stockProductForCreation));

        [HttpPut]
        public async Task<IActionResult> ModifyAsync([FromBody] StockProductForUpdateDto stockProductForUpdate)
            =>Ok(await _stockProductService.UpdateAsync(stockProductForUpdate));

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync(long  id)
            =>Ok(await _stockProductService.DeleteAsymc(id));


    }
}
