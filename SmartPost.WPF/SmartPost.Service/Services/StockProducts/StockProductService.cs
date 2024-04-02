using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.DTOs.StockProducts;
using SmartPost.Service.Interfaces.Brands;
using SmartPost.Service.Interfaces.Categories;
using SmartPost.Service.Interfaces.StockProducts;
using SmartPost.Service.Interfaces.Users;

namespace SmartPost.Service.Services.StockProducts;

public class StockProductService : IStockProductService
{
    private readonly IMapper _mapper;
    private readonly IBrandService _brandService;
    private readonly IProductRepository _productRepository;
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    private readonly IStockProductRepository _stockProductRepository;


    public StockProductService(IMapper mapper,
                               IStockProductRepository stockProductRepository,
                               IBrandService brandService,
                               ICategoryService categoryService,
                               IUserService userService,
                               IProductRepository productRepository)
    {
        _mapper = mapper;
        _brandService = brandService;
        _categoryService = categoryService;
        _stockProductRepository = stockProductRepository;
        _userService = userService;
        _productRepository = productRepository;
    }




    public async Task<bool> DeleteAsymc(long id)
    {
        var StockProduct = await _stockProductRepository.SelectAll()
            .Where(s => s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (StockProduct is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        return await _stockProductRepository.DeleteAsync(id);
    }


    public async Task<IEnumerable<StockProductsForResultDto>> GetAllAsync(PaginationParams @params)
    {
        var StockProducts = await _stockProductRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StockProductsForResultDto>>(StockProducts);
    }


    public async Task<StockProductsForResultDto> GetByIdAsync(long id)
    {
        var StockProduct = await _stockProductRepository.SelectAll()
           .Where(s => s.Id == id)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (StockProduct is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        return _mapper.Map<StockProductsForResultDto>(StockProduct);
    }


    public async Task<StockProductsForResultDto> UpdateAsync(long id, StockProductForUpdateDto updateDto)
    {
        var category = await _categoryService.RetrieveByIdAsync(updateDto.CategoryId);

        var brand = await _brandService.RetrieveByIdAsync(updateDto.BrandId);

        var stockProduct = await _stockProductRepository.SelectAll()
            .Where(s => s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (stockProduct is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        var mappedStockProduct = _mapper.Map(updateDto, stockProduct);
        mappedStockProduct.UpdatedAt = DateTime.UtcNow;

        var result = await _stockProductRepository.UpdateAsync(mappedStockProduct);

        return _mapper.Map<StockProductsForResultDto>(result);
    }
}
