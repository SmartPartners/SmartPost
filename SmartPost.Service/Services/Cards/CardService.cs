using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.Cards;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.DTOs.Cards;
using SmartPost.Service.Interfaces.Cards;
using SmartPost.Service.Interfaces.Users;

namespace SmartPost.Service.Services.Cards;
/// <summary>
/// Service tugagach Controller yozish
/// </summary>
public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CardService(
        IMapper mapper,
        ICardRepository cardRepository,
        IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
        _cardRepository = cardRepository;
    }

/*    public async Task<CardForResultDto> CreateAsync(CardForCreationDto dto)
    {
        var user = await _userService.RetrieveByIdAsync(dto.UserId);

        var card = await _cardRepository.SelectAll()
            .Where(c => c.PCode.ToLower() == dto.PCode.ToLower()
            && c.BarCode == dto.BarCode)
            .FirstOrDefaultAsync();

        if (card != null)
        {
            card.Quantity += dto.Quantity;
            await _cardRepository.UpdateAsync(card);

            //throw new CustomException(200, "Bu turdagi mahsulot bazada mavjudligi uchun uning soniga qo'shib qo'yildi.");
            return _mapper.Map<CardForResultDto>(card);
        }

        var mappedCard = _mapper.Map<Card>(dto);
        mappedCard.CreatedAt = DateTime.UtcNow;

        var result = await _cardRepository.InsertAsync(mappedCard);

        return _mapper.Map<CardForResultDto>(result);
    }*/

    /*public async Task<CardForResultDto> ModifyAsync(long id, CardForUpdateDto dto)
    {
        var card = await _cardRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (card is null)
            throw new CustomException(404, "Karta topilmadi.");

        var user = await _userService.RetrieveByIdAsync(dto.UserId);

        var mappedCard = _mapper.Map(dto, card);
        mappedCard.UpdatedAt = DateTime.UtcNow;

        var result = await _cardRepository.UpdateAsync(mappedCard);

        return _mapper.Map<CardForResultDto>(result);
    }*/

    public async Task<bool> ReamoveAsync(long id)
    {
        var userlanguage = await _cardRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (userlanguage is null)
            throw new CustomException(404, "Karta topilmadi.");

        await _cardRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<CardForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var cards = await _cardRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CardForResultDto>>(cards);
    }

    public async Task<CardForResultDto> RetrieveByIdAsync(long id)
    {
        var card = await _cardRepository.SelectAll()
           .Where(c => c.Id == id)
           .AsNoTracking()
           .FirstOrDefaultAsync();
        if (card is null)
            throw new CustomException(404, "Karta topilmadi.");

        return _mapper.Map<CardForResultDto>(card);
    }
}