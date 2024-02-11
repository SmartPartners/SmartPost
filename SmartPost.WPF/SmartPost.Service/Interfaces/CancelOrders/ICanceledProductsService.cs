using SmartPost.Service.DTOs.CancelOrders;

namespace SmartPost.Service.Interfaces.CancelOrders;

public interface ICanceledProductsService
{
    public Task<bool> CanceledProductsAsync(long id, decimal quantity, long canceledBy, string reason, bool action);
    public Task<bool> CanceledProductsFromPArterAsync(long id, long partnerId, decimal quantity, long canceledBy, string reason, bool action);
    public Task<IEnumerable<CancelOrderForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);
    public Task<IEnumerable<CancelOrderForResultDto>> RetrieveAllWithMaxSaledAsync(DateTime startDate, DateTime endDate);
}
