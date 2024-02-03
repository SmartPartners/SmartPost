using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Partners;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.Users;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.Partners;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.DTOs.PartnerProduct;
using SmartPost.Service.DTOs.StockProducts;
using SmartPost.Service.Interfaces.Brands;
using SmartPost.Service.Interfaces.Categories;
using SmartPost.Service.Interfaces.Partners;
using SmartPost.Service.Interfaces.Users;

namespace SmartPost.Service.Services.Partners;

public class PartnerProductService : IPartnerProductService
{
    private readonly IPartnerProductRepository _partnerProductRepository;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly IProductRepository _productRepository;
    private readonly IUserService _userService;
    private readonly IPartnerService _partnerService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public PartnerProductService(
        IPartnerProductRepository partnerProductRepository,
        IMapper mapper,
        IProductRepository productRepository,
        IUserRepository userRepository,
        ICategoryService categoryService,
        IBrandService brandService,
        IUserService userService,
        IPartnerService partnerService)
    {
        _partnerProductRepository = partnerProductRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _categoryService = categoryService;
        _brandService = brandService;
        _userService = userService;
        _partnerService = partnerService;
    }

    public async Task<bool> MoveProductToPartnerProductAsync(long id, long partnerId, long userId, decimal quantityToMove)
    {
        var insufficientProduct = await _productRepository.SelectAll()
            .Where(p => p.Id == id && p.Quantity < quantityToMove)
            .FirstOrDefaultAsync();
        if (insufficientProduct is not null)
            throw new CustomException(400, $"Omborda buncha mahsulot mavjud emas.\nOmborda {insufficientProduct.Quantity} ta mahsulot bor.");

        if (quantityToMove == 0)
            throw new CustomException(404, "Mahsulotni hajmini kiriting.");

        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (product == null || product.Quantity == 0)
            throw new CustomException(404, "Bu turdagi mahsulot omborda mavjud emas.");

        var partnerProduct = new PartnerProduct
        {
            PCode = product.PCode,
            BarCode = product.BarCode,
            ProductName = product.ProductName,
            Price = product.Price,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            UserId = userId,
            PartnerId = partnerId,
            Status = "Nasiya",
            Quantity = quantityToMove,
            CreatedAt = DateTime.UtcNow,
        };
        partnerProduct.TotalPrice = partnerProduct.Price * partnerProduct.Quantity;
        partnerProduct.Debt = partnerProduct.TotalPrice;
        partnerProduct.Paid = 0;

        var partnerProducts = await _partnerProductRepository.SelectAll()
            .Where(p => p.PCode == product.PCode && p.BarCode == product.BarCode)
            .FirstOrDefaultAsync();
        if (partnerProducts is not null)
        {
            partnerProducts.Quantity += quantityToMove;
            partnerProducts.UpdatedAt = DateTime.UtcNow;
            await _partnerProductRepository.UpdateAsync(partnerProducts);

            product.Quantity -= quantityToMove;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }

        await _partnerProductRepository.InsertAsync(partnerProduct);

        product.Quantity -= quantityToMove;
        product.UpdatedAt = DateTime.UtcNow;
        await _productRepository.UpdateAsync(product);

        return true;
    }

    public async Task<bool> PayForProductsAsync(long productId, long partnerId, decimal paid)
    {
        var partnerProduct = await _partnerProductRepository.SelectAll()
            .Where(p => p.Id == productId && p.PartnerId == partnerId)
            .FirstOrDefaultAsync();
        if (partnerProduct is not null)
        {
            var nat = partnerProduct.Debt -= paid;
            partnerProduct.Debt = nat;
            partnerProduct.Paid += paid;
            partnerProduct.UpdatedAt = DateTime.UtcNow;
            await _partnerProductRepository.UpdateAsync(partnerProduct);


            if (partnerProduct.Debt == 0 && partnerProduct.Paid == partnerProduct.TotalPrice)
            {
                partnerProduct.Status = "To'landi";
                partnerProduct.UpdatedAt = DateTime.UtcNow;
                await _partnerProductRepository.UpdateAsync(partnerProduct);
            }
        }
        return true;
    }



    public async Task<PartnerProductForResultDto> ModifyAsync(long id, PartnerProductForUpdateDto dto)
    {
        var category = await _categoryService.RetrieveByIdAsync(dto.CategoryId);

        var brand = await _brandService.RetrieveByIdAsync(dto.BrandId);

        var user = await _userService.RetrieveByIdAsync(dto.UserId);

        var partner = await _partnerService.RetrieveByIdAsync(dto.PartnerId);

        var partnerProduct = await _partnerProductRepository.SelectAll()
            .Where(s => s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (partner is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        var mappedStockProduct = _mapper.Map(dto, partnerProduct);
        mappedStockProduct.UpdatedAt = DateTime.UtcNow;

        var result = await _partnerProductRepository.UpdateAsync(mappedStockProduct);

        return _mapper.Map<PartnerProductForResultDto>(result);
    }


    public async Task<bool> RemoveAsync(long id)
    {
        var StockProduct = await _partnerProductRepository.SelectAll()
            .Where(s => s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (StockProduct is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        return await _partnerProductRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PartnerProductForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var partnerProducts = await _partnerProductRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PartnerProductForResultDto>>(partnerProducts);
    }

    public async Task<IEnumerable<StockProductsForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
    {
        if (userId != null)
        {
            var product = await _partnerProductRepository.SelectAll()
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate && p.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        var products = await _partnerProductRepository.SelectAll()
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<StockProductsForResultDto>>(products);
    }

    public async Task<PartnerProductForResultDto> RetrieveByIdAsync(long id)
    {
        var StockProduct = await _partnerProductRepository.SelectAll()
          .Where(s => s.Id == id)
          .AsNoTracking()
          .FirstOrDefaultAsync();

        if (StockProduct is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        return _mapper.Map<PartnerProductForResultDto>(StockProduct);
    }
}
