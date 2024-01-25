using Microsoft.AspNetCore.Mvc;
using SmartPost.Api.Controllers.Commons;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.CancelOrders;
using SmartPost.Service.Interfaces.CancelOrders;

namespace SmartPost.Api.Controllers.CancelOrders
{
    public class CancelOrdersController : BaseController
    {
        ICancelOrderService _cancelOrderService;

        public CancelOrdersController(ICancelOrderService cancelOrderService)
        {
            _cancelOrderService = cancelOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CancelOrderForCreationDto dto)
           => Ok(await _cancelOrderService.CreateAsync(dto));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _cancelOrderService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
            => Ok(await _cancelOrderService.RetrieveByIdAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id, [FromBody] CancelOrderForUpdateDto dto)
            => Ok(await _cancelOrderService.ModifyAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
            => Ok(await _cancelOrderService.ReamoveAsync(id));
    }
}
