using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Barnds;
using SmartPost.DataAccess.Interfaces.Categories;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.DTOs.StockProducts;
using SmartPost.Service.Interfaces.StockProducts;

namespace SmartPost.Service.Services.StockProducts;

public class StockProductService : IStockProductService
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStockProductRepository _stockProductRepository; 


    public StockProductService(IMapper mapper, IStockProductRepository stockProductRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
        _stockProductRepository = stockProductRepository;
    }

    public async Task<StockProductsForResultDto> CreateAsync(StockProductForCreationDto createDto)
    {

        var category = await _categoryRepository.SelectAll()
           .Where(c => c.Id == createDto.CategoryId)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (category is null)
            throw new CustomException(404, "Category is not found");

        var brand = await _brandRepository.SelectAll()
             .Where(b => b.Id == createDto.BrandId)
        .FirstOrDefaultAsync();

        if (brand is null)
            throw new CustomException(404, "Brand is not found");
        
        var stockProduct = await _stockProductRepository.SelectAll()
            .Where(s => s.PCode == createDto.PCode || s.ProductName == createDto.ProductName || s.BarCode == createDto.BarCode)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (stockProduct is null)
        {
            var mappedStockProduct = _mapper.Map<StokProduct>(createDto);
            mappedStockProduct.CreatedAt = DateTime.UtcNow;
            return _mapper.Map<StockProductsForResultDto>(await _stockProductRepository.InsertAsync(mappedStockProduct));
        }

        stockProduct.Quantity += createDto.Quantity;
        return _mapper.Map<StockProductsForResultDto>(await _stockProductRepository.UpdateAsync(stockProduct));
    }

    public async Task<bool> DeleteAsymc(long id)
    {
        var StockProduct = await _stockProductRepository.SelectAll()
            .Where(s=>s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (StockProduct is null)
            throw new CustomException(404, "StockProduct is not found");

         return await _stockProductRepository.DeleteAsync(id);
    }
       

    public async Task<IEnumerable<StockProductsForResultDto>> GetAllAsync()
        => _mapper.Map<IEnumerable<StockProductsForResultDto>>(await _stockProductRepository.SelectAll().AsNoTracking().ToListAsync());
   

    public async Task<StockProductsForResultDto> GetByIdAsync(long id)
    {
        var StockProduct = await _stockProductRepository.SelectAll()
           .Where(s => s.Id == id)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (StockProduct is null)
            throw new CustomException(404, "StockProduct is not found");

        return _mapper.Map<StockProductsForResultDto>(StockProduct);
    }
   

    public async Task<StockProductsForResultDto> UpdateAsync(StockProductForUpdateDto updateDto)
    {

        var category = await _categoryRepository.SelectAll()
          .Where(c => c.Id == updateDto.CategoryId)
          .AsNoTracking()
          .FirstOrDefaultAsync();

        if (category is null)
            throw new CustomException(404, "Category is not found");

        var brand = await _brandRepository.SelectAll()
             .Where(b => b.Id == updateDto.BrandId)
             .FirstOrDefaultAsync();

        if (brand is null)
            throw new CustomException(404, "Brand is not found");

        var stockProduct = await _stockProductRepository.SelectAll()
            .Where(s => s.Id == updateDto.Id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (stockProduct is null)
            throw new CustomException(404, "StockProduct is not found");

        var mappedStockProduct = _mapper.Map<StokProduct>(updateDto);
        mappedStockProduct.UpdatedAt = DateTime.UtcNow;

        return _mapper.Map<StockProductsForResultDto>(await _stockProductRepository.UpdateAsync(mappedStockProduct));
    }

    public async Task<StockProductsForResultDto>AddQuentityToStockProduct(long id, decimal quantity)
    {
        var stockProduct = await _stockProductRepository.SelectAll()
            .Where(s => s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (stockProduct is null)
            throw new CustomException(404, "StockProduct is not found");

        stockProduct.Quantity += quantity;
        return _mapper.Map<StockProductsForResultDto>(await _stockProductRepository.UpdateAsync(stockProduct));
    }
}
