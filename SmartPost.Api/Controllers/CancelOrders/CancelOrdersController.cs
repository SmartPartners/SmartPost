using Microsoft.AspNetCore.Mvc;
using SmartPost.Api.Controllers.Commons;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.CancelOrders;
using SmartPost.Service.Interfaces.CancelOrders;

namespace SmartPost.Api.Controllers.CancelOrders
{
    public class CancelOrdersController : BaseController
    {
        private readonly ICancelOrderService _cancelOrderService;
        private readonly ICanceledProductsService _productsService;

        public CancelOrdersController(ICancelOrderService cancelOrderService, ICanceledProductsService productsService)
        {
            _cancelOrderService = cancelOrderService;
            _productsService = productsService;
        }

        [HttpPost("magazinda-sotilgan-mahsulotlar-uchun")]
        public async Task<IActionResult> PostAsync(long id, decimal quantity, long canceledBy, string reason, bool action)
           => Ok(await _productsService.CanceledProductsAsync(id, quantity, canceledBy, reason, action));

        [HttpPost("hamkorlardan-qaytgan-mahsulotlar-uchun")]
        public async Task<IActionResult> PostPartnetCanceledProductsAsync(long id, long partnerId, decimal quantity, long canceledBy, string reason, bool action)
           => Ok(await _productsService.CanceledProductsFromPArterAsync(id, partnerId, quantity, canceledBy, reason, action));

        [HttpGet("ikkita-vaqt-orasida-magazindagi-mahsulotlarni-kurish/{userId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAllAsync(long userId, DateTime startDate, DateTime endDate)
            => Ok(await _productsService.RetrieveAllWithDateTimeAsync(userId, startDate, endDate));

        [HttpGet("yaroqsiz-mahsulotlarni-korish/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAllYaroqsizAsync(DateTime startDate, DateTime endDate)
            => Ok(await _productsService.RetrieveAllWithMaxSaledAsync(startDate, endDate));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _cancelOrderService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
            => Ok(await _cancelOrderService.RetrieveByIdAsync(id));

       /* [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id, [FromBody] CancelOrderForUpdateDto dto)
            => Ok(await _cancelOrderService.ModifyAsync(id, dto));*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
            => Ok(await _cancelOrderService.ReamoveAsync(id));
    }
}
