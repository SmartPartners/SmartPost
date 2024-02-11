using Microsoft.AspNetCore.Mvc;
using SmartPost.Api.Controllers.Commons;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.StockProducts;
using SmartPost.Service.Interfaces.StockProducts;

namespace SmartPost.Api.Controllers.StockProducts
{
    public class StockProductController : BaseController
    {
        private readonly IStockProductService _stockProductService;
        private readonly IProductStockManagementService _managementService;
        public StockProductController(IStockProductService stockProductService, IProductStockManagementService managementService)
        {
            _stockProductService = stockProductService;
            _managementService = managementService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _stockProductService.GetByIdAsync(id));

        /* [HttpPost]
         public async Task<IActionResult> AddsAsync(StockProductForCreationDto dto)
             => Ok(await _stockProductService.CreateAsync(dto));*/

        [HttpGet]
        public async Task<IActionResult> GeAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _stockProductService.GetAllAsync(@params));

        [HttpPost("ombordan-mahsulotni-magazinga-kochirish")]
        public async Task<IActionResult> AddAsync(long id, long userId, decimal quantityToMove)
            => Ok(await _managementService.MoveProductToStockAsync(id, userId, quantityToMove));

        [HttpGet("ikkita-vaqt-orasida-magazindagi-mahsulotlarni-kurish/{userId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAllAsync(long userId, DateTime startDate, DateTime endDate)
            => Ok(await _managementService.RetrieveAllWithDateTimeAsync(userId, startDate, endDate));

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyAsync(long id, [FromBody] StockProductForUpdateDto stockProductForUpdate)
            => Ok(await _stockProductService.UpdateAsync(id, stockProductForUpdate));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _stockProductService.DeleteAsymc(id));


    }
}
