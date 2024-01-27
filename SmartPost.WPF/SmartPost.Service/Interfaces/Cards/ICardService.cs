using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.Cards;

namespace SmartPost.Service.Interfaces.Cards;
/// <summary>
/// Service ni yaratish
/// </summary>
public interface ICardService
{
    Task<bool> ReamoveAsync(long id);
    Task<CardForResultDto> RetrieveByIdAsync(long id);
    Task<CardForResultDto> CreateAsync(CardForCreationDto dto);
    Task<CardForResultDto> ModifyAsync(long id, CardForUpdateDto dto);
    Task<IEnumerable<CardForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
