using SmartPost.Service.DTOs.Cards;

namespace SmartPost.Service.Interfaces.Cards;

public interface ICardManagementService
{
    public Task<bool> MoveProductToStockAsync(long id, long userId, decimal quantityToMove, string trnasNo);
    public Task<bool> UpdateWithTransactionNumberAsync(string transactionNumber);
    public string GenerateTransactionNumber();
    public Task<bool> CalculeteDiscountPercentageAsync(long id, short discountPercentage);
    public Task<IEnumerable<CardForResultDto>> GetByBarCodeAsync(string barCode);
    public Task<IEnumerable<CardForResultDto>> SvetUchgandaAsync(string status);
    public Task<IEnumerable<CardForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);
    public Task<IEnumerable<CardForResultDto>> RetrieveAllWithMaxSaledAsync(int takeMax);
}
