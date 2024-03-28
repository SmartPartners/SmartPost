using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.DTOs.Products;
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
            SalePrice = product.SalePrice ?? 0,
            PercentageSalePrice = product.PercentageSalePrice ?? 0,
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
            stokProducts.UpdatedAt = DateTime.UtcNow;
            await _stockProductRepository.UpdateAsync(stokProducts);

            product.Quantity -= quantityToMove;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);

            return true;
        }

        await _stockProductRepository.InsertAsync(stockProduct);

        stockProduct.Status = "Qo'shildi";
        stockProduct.UpdatedAt = DateTime.UtcNow;
        await _stockProductRepository.UpdateAsync(stockProduct);


        product.Quantity -= quantityToMove;
        product.UpdatedAt = DateTime.UtcNow;
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



    public async Task<StockProductsForResultDto> InsertWithBarCodeAsync(string barCode, decimal quantity)
    {
        var product = await _stockProductRepository.SelectAll()
             .Where(p => p.BarCode == barCode)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        var mappedProduct = new StokProduct
        {
            Id = product.Id,
            UserId = product.UserId,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            ProductName = product.ProductName,
            BarCode = product.BarCode,
            PCode = product.PCode,
            Price = product.Price,
            SalePrice = product.SalePrice,
            PercentageSalePrice = product.PercentageSalePrice,
            Quantity = quantity,
            Status = product.Status,
            CreatedAt = DateTime.UtcNow
        };

        var stock = await _productRepository.SelectAll()
           .Where(s => s.PCode == mappedProduct.PCode && s.BarCode == mappedProduct.BarCode)
           .FirstOrDefaultAsync();
        if (stock != null)
        {
            stock.Quantity -= quantity;
            stock.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(stock);
        }

        product.Quantity += quantity;
        product.UpdatedAt = DateTime.UtcNow;
        await _stockProductRepository.UpdateAsync(product);

        return _mapper.Map<StockProductsForResultDto>(mappedProduct);
    }

}



