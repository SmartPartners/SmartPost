using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.CancelOrders;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.CancelOrders;
using SmartPost.Domain.Entities.Cards;
using SmartPost.Service.Commons.Exceptions;

namespace SmartPost.Service.Services.CancelOrders;

public class CanceledProductsService
{
    private readonly ICancelOrderRepository _cancelOrderRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IStockProductRepository _stockProductRepository;

    public CanceledProductsService(
        ICancelOrderRepository cancelOrderRepository, 
        ICardRepository cardRepository,
        IStockProductRepository stockProductRepository)
    {
        _cancelOrderRepository = cancelOrderRepository;
        _cardRepository = cardRepository;
        _stockProductRepository = stockProductRepository;
    }

    public async Task<bool> CanceledProductsAsync(long id, decimal quantity, string canceledBy, string reason, bool action)
    {
        var card = await _cardRepository.SelectAll()
            .Where(c => c.Id == id && c.Quantity < quantity)
            .FirstOrDefaultAsync();

        if (card is not null)
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
            ReturnDate = DateTime.UtcNow
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
            }
        }
        else
        {
            canceledOrder.Status = "Yaroqsiz";
            await _cancelOrderRepository.UpdateAsync(canceledOrder);
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

    public async Task<IEnumerable<CancelOrder>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
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

        return products;
    }

    /// <summary>
    /// Yaroqsizlarni qaytaradi
    /// </summary>
    /// <returns></returns>

    public async Task<IEnumerable<CancelOrder>> RetrieveAllWithMaxSaledAsync(DateTime startDate, DateTime endDate)
    {
        var result = await _cancelOrderRepository.SelectAll()
            .Where(c => c.CreatedAt >= startDate && c.CreatedAt <= endDate)
            .Where(c => c.Status == "Yaroqsiz")
            .AsNoTracking()
            .ToListAsync();

        return result;
    }
}
