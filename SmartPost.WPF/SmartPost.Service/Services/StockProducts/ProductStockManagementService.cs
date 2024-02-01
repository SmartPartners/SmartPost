using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.DTOs.StockProducts;
using SmartPost.Service.Interfaces.StockProducts;

namespace SmartPost.Service.Services.StockProducts;

public class ProductStockManagementService : IProductStockManagementService
{
    private readonly IProductRepository _productRepository;
    private readonly IStockProductRepository _stockProductRepository;
    private readonly IMapper _mapper;

    public ProductStockManagementService(IProductRepository productRepository, IStockProductRepository stockProductRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _stockProductRepository = stockProductRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Ombordan magazinga mahsulotni olib o'tish
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <param name="quantityToMove"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<bool> MoveProductToStockAsync(long id, long userId, decimal quantityToMove)
    {
        var insufficientProduct = await _productRepository.SelectAll()
            .Where(q => q.Id == id && q.Quantity < quantityToMove)
            .FirstOrDefaultAsync();

        if (insufficientProduct != null)
            throw new CustomException(400, $"Omborda buncha mahsulot mavjud emas.\nOmborda {insufficientProduct.Quantity} ta mahsulot bor.");

        if (quantityToMove == 0)
            throw new CustomException(404, "Mahsulotni hajmini kiriting.");

        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (product == null || product.Quantity == 0)
            throw new CustomException(404, "Bu turdagi mahsulot omborda mavjud emas.");

        var stockProduct = new StokProduct
        {
            PCode = product.PCode,
            BarCode = product.BarCode,
            ProductName = product.ProductName,
            Price = product.Price,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            UserId = userId,
            Status = "Kutilmoqda",
            Quantity = quantityToMove,
            CreatedAt = DateTime.UtcNow
        };

        var stokProducts = await _stockProductRepository.SelectAll()
            .Where(p => p.PCode == product.PCode && p.BarCode == product.BarCode)
            .FirstOrDefaultAsync();

        if (stokProducts is not null)
        {
            stokProducts.Quantity += quantityToMove;
            stokProducts.Status = "Qo'shildi";
            await _stockProductRepository.UpdateAsync(stokProducts);

            product.Quantity -= quantityToMove;
            await _productRepository.UpdateAsync(product);

            return true;
        }

        await _stockProductRepository.InsertAsync(stockProduct);

        stockProduct.Status = "Qo'shildi";
        await _stockProductRepository.UpdateAsync(stockProduct);


        product.Quantity -= quantityToMove;
        await _productRepository.UpdateAsync(product);

        return true;
    }

    /// <summary>
    /// Ikkita vaqt oralig'idagi mahsulotlarni chiqarib beradi
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<StockProductsForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
    {
        if (userId != null)
        {
            var product = await _stockProductRepository.SelectAll()
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate && p.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        var products = await _stockProductRepository.SelectAll()
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .AsNoTracking()
            .ToListAsync();

        return  _mapper.Map<IEnumerable<StockProductsForResultDto>>(products);
    }



}



