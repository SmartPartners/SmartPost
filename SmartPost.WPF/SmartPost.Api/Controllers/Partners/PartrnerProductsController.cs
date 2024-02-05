using Microsoft.AspNetCore.Mvc;
using SmartPost.Api.Controllers.Commons;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.PartnerProduct;
using SmartPost.Service.DTOs.StockProducts;
using SmartPost.Service.Interfaces.Partners;

namespace SmartPost.Api.Controllers.Partners
{
    public class PartrnerProductsController : BaseController
    {
        private readonly IPartnerProductService _partnerProductService;

        public PartrnerProductsController(IPartnerProductService partnerProductService)
        {
            _partnerProductService = partnerProductService;
        }

        [HttpPost("ombordan-hamkorga-yuk-jonatish")]
        public async Task<IActionResult> AddAsync(long productId, long partnerId, long userId, decimal quantityToMove, string transNo)
            => Ok(await _partnerProductService.MoveProductToPartnerProductAsync(productId, partnerId, userId, quantityToMove, transNo));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _partnerProductService.RetrieveByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GeAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _partnerProductService.RetrieveAllAsync(@params));

        [HttpGet("transaction-generator")]
        public async Task<IActionResult> GenerateTranNo()
           => Ok(await Task.FromResult(_partnerProductService.GenerateTransactionNumber()));

        [HttpPut("update-partner-products-paid")]
        public async Task<IActionResult> UpdatePaidAsync(long partnerId, decimal paid)
            => Ok(await _partnerProductService.PayForProductsAsync(partnerId, paid));

        [HttpGet("ikkita-vaqt-orasida-magazindagi-mahsulotlarni-kurish/{userId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAllAsync(long userId, DateTime startDate, DateTime endDate)
            => Ok(await _partnerProductService.RetrieveAllWithDateTimeAsync(userId, startDate, endDate));

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyAsync(long id, [FromBody] PartnerProductForUpdateDto stockProductForUpdate)
            => Ok(await _partnerProductService.ModifyAsync(id, stockProductForUpdate));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _partnerProductService.RemoveAsync(id));
    }
}
