using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.PartnerProduct;
using SmartPost.Service.DTOs.StockProducts;

namespace SmartPost.Service.Interfaces.Partners;

public interface IPartnerProductService
{
    Task<bool> RemoveAsync(long id);
    string GenerateTransactionNumber();
    Task<PartnerProductForResultDto> RetrieveByIdAsync(long id);
    Task<bool> PayForProductsAsync(long partnerId, decimal paid);
    Task<PartnerProductForResultDto> RetrieveByTransNoAsync(string transNo);
    Task<PartnerProductForResultDto> ModifyAsync(long id, PartnerProductForUpdateDto dto);
    Task<IEnumerable<PartnerProductForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<bool> MoveProductToPartnerProductAsync(long id, long partnerId, long userId, decimal quantityToMove, string transNo);
    Task<IEnumerable<StockProductsForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);
}
