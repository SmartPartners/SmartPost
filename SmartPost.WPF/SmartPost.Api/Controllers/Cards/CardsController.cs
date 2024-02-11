using Microsoft.AspNetCore.Mvc;
using SmartPost.Api.Controllers.Commons;
using SmartPost.Domain.Configurations;
using SmartPost.Service.Interfaces.Cards;

namespace SmartPost.Api.Controllers.Cards
{
    public class CardsController : BaseController
    {
        private readonly ICardService _cardService;
        private readonly ICardManagementService _cardManagement;

        public CardsController(ICardService cardService, ICardManagementService cardManagement)
        {
            _cardService = cardService;
            _cardManagement = cardManagement;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(long id, long userId, decimal quantityToMove, string trnasNo)
           => Ok(await _cardManagement.MoveProductToStockAsync(id, userId, quantityToMove, trnasNo));

        [HttpPost("calculate-discount-percentage/{id}/{discountPercentage}")]
        public async Task<IActionResult> CalculateAsync(long id, short discountPercentage)
           => Ok(await _cardManagement.CalculeteDiscountPercentageAsync(id, discountPercentage));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _cardService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
            => Ok(await _cardService.RetrieveByIdAsync(id));

        [HttpPost("{transNo}")]
        public async Task<IActionResult> Updateasync([FromRoute(Name = "transNo")] string transNo)
            => Ok(await _cardManagement.UpdateWithTransactionNumberAsync(transNo));

        [HttpGet("bar-code-orqali-olish/{barCode}")]
        public async Task<IActionResult> GetByBarCodeAsync([FromRoute(Name = "barCode")] string barCode)
            => Ok(await _cardManagement.GetByBarCodeAsync(barCode));

        [HttpGet("status-orqali-olish/{status}")]
        public async Task<IActionResult> GetBystatusAsync([FromRoute(Name = "status")] string status)
            => Ok(await _cardManagement.SvetUchgandaAsync(status));

        [HttpGet("ikkita-vaqt-orasida-magazindagi-mahsulotlarni-kurish/{userId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAllCardProductsAsync(long userId, DateTime startDate, DateTime endDate)
            => Ok(await _cardManagement.RetrieveAllWithDateTimeAsync(userId, startDate, endDate));

        [HttpGet("eng-kop-sotilgan-yuklarni-olish/{max}")]
        public async Task<IActionResult> GetByMaxAsync([FromRoute(Name = "max")] int max)
            => Ok(await _cardManagement.RetrieveAllWithMaxSaledAsync(max));

        [HttpGet("transaction-generator")]
        public async Task<IActionResult> GenerateTranNo()
            => Ok(await Task.FromResult(_cardManagement.GenerateTransactionNumber()));

        /* [HttpPut("{id}")]
         public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id, [FromBody] CardForUpdateDto dto)
             => Ok(await _cardService.ModifyAsync(id, dto));*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
            => Ok(await _cardService.ReamoveAsync(id));
    }
}
