using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.CancelOrders;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.DataAccess.Interfaces.Partners;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.CancelOrders;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.DTOs.CancelOrders;
using SmartPost.Service.Interfaces.CancelOrders;

namespace SmartPost.Service.Services.CancelOrders;

public class CanceledProductesService : ICanceledProductsService
{
    private readonly ICancelOrderRepository _cancelOrderRepository;
    private readonly IPartnerRepository _partnerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IStockProductRepository _stockProductRepository;
    private readonly IPartnerProductRepository _partnerProductRepository;
    private readonly IMapper _mapper;
    private readonly ICardRepository _cardRepository;

    public CanceledProductesService(ICancelOrderRepository cancelOrderRepository,
        IMapper mapper,
        ICardRepository cardRepository,
        IStockProductRepository stockProductRepository,
        IPartnerProductRepository partnerProductRepository,
        IProductRepository productRepository,
        IPartnerRepository partnerRepository)
    {
        _cancelOrderRepository = cancelOrderRepository;
        _mapper = mapper;
        _cardRepository = cardRepository;
        _stockProductRepository = stockProductRepository;
        _partnerProductRepository = partnerProductRepository;
        _productRepository = productRepository;
        _partnerRepository = partnerRepository;
    }

    /// <summary>
    /// Magazindan sotilgan yuklarni cancel qilish
    /// </summary>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <param name="canceledBy"></param>
    /// <param name="reason"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
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
                stok.UpdatedAt = DateTime.UtcNow;
                await _stockProductRepository.UpdateAsync(stok);

                canceledOrder.Status = "Yaroqli";
                canceledOrder.UpdatedAt = DateTime.UtcNow;
                await _cancelOrderRepository.UpdateAsync(canceledOrder);
            }
        }
        else
        {
            canceledOrder.Status = "Yaroqsiz";
            canceledOrder.UpdatedAt = DateTime.UtcNow;
            await _cancelOrderRepository.UpdateAsync(canceledOrder);
        }


        return true;
}

    /// <summary>
    /// Hamkorlardan qaytgan mahsulot uchun
    /// </summary>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <param name="canceledBy"></param>
    /// <param name="reason"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<bool> CanceledProductsFromPArterAsync(long id, long partnerId, decimal quantity, long canceledBy, string reason, bool action)
    {
        var partnerProduct = await _partnerProductRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (partnerProduct is not null && partnerProduct.Quantity < quantity)
            throw new CustomException(404, $"Noto'g'ri son kirityabsiz, sotilgan yuklar soni: {partnerProduct.Quantity}");


        var canceledOrder = new CancelOrder
        {
            SaleBy = partnerProduct.UserId,
            TransNo = partnerProduct.TransNo,
            ProductName = partnerProduct.ProductName,
            BarCode = partnerProduct.BarCode,
            PCode = partnerProduct.PCode,
            Price = partnerProduct.Price,
            TotalPrice = partnerProduct.TotalPrice,
            Quantity = quantity,
            CanceledBy = canceledBy,
            Reason = reason,
            Action = action,
            ReturnDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        var totalPrice = canceledOrder.Price * canceledOrder.Quantity;

        var partDebt = await _partnerRepository.SelectAll()
            .Where(p => p.Id == partnerId)
            .FirstOrDefaultAsync();
        if(partDebt.Debt != 0)
        {
            partDebt.Debt -= totalPrice;
            await _partnerRepository.UpdateAsync(partDebt);
        }
        

        await _cancelOrderRepository.InsertAsync(canceledOrder);

        if (canceledOrder.Action)
        {
            var stok = await _productRepository.SelectAll()
                .Where(s => s.BarCode == canceledOrder.BarCode)
                .FirstOrDefaultAsync();

            if (stok is not null)
            {
                stok.Quantity += quantity;
                stok.UpdatedAt = DateTime.UtcNow;
                await _productRepository.UpdateAsync(stok);

                canceledOrder.Status = "Yaroqli";
                canceledOrder.UpdatedAt = DateTime.UtcNow;
                await _cancelOrderRepository.UpdateAsync(canceledOrder);

                partnerProduct.Quantity -= quantity;
                partnerProduct.UpdatedAt = DateTime.UtcNow;
                await _partnerProductRepository.UpdateAsync(partnerProduct);
            }
        }
        else
        {
            canceledOrder.Status = "Yaroqsiz";
            canceledOrder.UpdatedAt = DateTime.UtcNow;
            await _cancelOrderRepository.UpdateAsync(canceledOrder);

            partnerProduct.Quantity -= quantity;
            partnerProduct.UpdatedAt = DateTime.UtcNow;
            await _partnerProductRepository.UpdateAsync(partnerProduct);
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

