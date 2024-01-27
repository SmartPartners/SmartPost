using Microsoft.AspNetCore.Mvc;
using SmartPost.Api.Controllers.Commons;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.InventoryLists;
using SmartPost.Service.Interfaces.InventoryLists;

namespace SmartPost.Api.Controllers.InventoryLists
{
    public class InventoryListsController : BaseController
    {
        private readonly IInventoryListService _inventoryListService;

        public InventoryListsController(IInventoryListService inventoryListService)
        {
            this._inventoryListService = inventoryListService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] InventoryListForCreationDto dto)
           => Ok(await _inventoryListService.CreateAsync(dto));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _inventoryListService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
            => Ok(await _inventoryListService.RetrieveByIdAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id, [FromBody] InventoryListForUpdateDto dto)
            => Ok(await _inventoryListService.ModifyAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
            => Ok(await _inventoryListService.ReamoveAsync(id));
    }
}
