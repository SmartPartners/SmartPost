using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.CancelOrders;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.CancelOrders;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.DTOs.CancelOrders;
using SmartPost.Service.Interfaces.CancelOrders;

namespace SmartPost.Service.Services.CancelOrders;

public class CanceledProductesService : ICanceledProductsService
{
    private readonly ICancelOrderRepository _cancelOrderRepository;
    private readonly IStockProductRepository _stockProductRepository;
    private readonly IMapper _mapper;
    private readonly ICardRepository _cardRepository;

    public CanceledProductesService(ICancelOrderRepository cancelOrderRepository, IMapper mapper, ICardRepository cardRepository, IStockProductRepository stockProductRepository)
    {
        _cancelOrderRepository = cancelOrderRepository;
        _mapper = mapper;
        _cardRepository = cardRepository;
        _stockProductRepository = stockProductRepository;
    }

    public async Task<bool> CanceledProductsAsync(long id, decimal quantity, long canceledBy, string reason, bool action)
    {
        var card = await _cardRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (card is not null && card.Quantity < quantity)
            throw new CustomException(404, $"Noto'g'ri son kirityabsiz, sotilgan yuklar soni: {card.Quantity}");


        var canceledOrder = new CancelOrder
        {
            SaleBy = card.UserId,
            TransNo = card.TransNo,
            ProductName = card.ProductName,
            BarCode = card.BarCode,
            PCode = card.PCode,
            Price = card.Price,
            TotalPrice = card.TotalPrice,
            Quantity = quantity,
            CanceledBy = canceledBy,
            Reason = reason,
            Action = action,
            ReturnDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        await _cancelOrderRepository.InsertAsync(canceledOrder);

        if (canceledOrder.Action)
        {
            var stok = await _stockProductRepository.SelectAll()
                .Where(s => s.BarCode == canceledOrder.BarCode)
                .FirstOrDefaultAsync();

            if (stok is not null)
            {
                stok.Quantity += quantity;
                await _stockProductRepository.UpdateAsync(stok);

                canceledOrder.Status = "Yaroqli";
                await _cancelOrderRepository.UpdateAsync(canceledOrder);

                card.Quantity -= quantity;
                await _cardRepository.UpdateAsync(card);
            }
        }
        else
        {
            canceledOrder.Status = "Yaroqsiz";
            await _cancelOrderRepository.UpdateAsync(canceledOrder);

            card.Quantity -= quantity;
            await _cardRepository.UpdateAsync(card);
        }


        return true;
}

    /// <summary>
    /// Cancel qilingan mahsulotlarni chiqarib beradi
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>

    public async Task<IEnumerable<CancelOrderForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
    {
        if (userId != null)
        {
            var product = await _cancelOrderRepository.SelectAll()
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate && p.SaleBy == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        var products = await _cancelOrderRepository.SelectAll()
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<CancelOrderForResultDto>>(products);
    }

    /// <summary>
    /// Yaroqsizlarni qaytaradi
    /// </summary>
    /// <returns></returns>

    public async Task<IEnumerable<CancelOrderForResultDto>> RetrieveAllWithMaxSaledAsync(DateTime startDate, DateTime endDate)
    {
        var result = await _cancelOrderRepository.SelectAll()
            .Where(c => c.CreatedAt >= startDate && c.CreatedAt <= endDate)
            .Where(c => c.Status == "Yaroqsiz")
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<CancelOrderForResultDto>>(result);
    }
}

