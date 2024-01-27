using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.Service.DTOs.Products;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.Barnds;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.Service.Interfaces.Categories;
using SmartPost.Domain.Configurations;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.Interfaces.Brands;

namespace SmartPost.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IBrandService _brandService;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryService _categoryService;

    public ProductService(IMapper mapper,
                          IProductRepository productRepository, 
                           ICategoryService categoryService, 
                           IBrandService brandService)
    {
        this._mapper = mapper;
        this._brandService = brandService;
        this._categoryService = categoryService;
        this._productRepository = productRepository;
    }

    public async Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto)
    {
        var category = await _categoryService.RetrieveByIdAsync(productForCreationDto.CategoryId);

        var brand = await _brandService.RetrieveByIdAsync(productForCreationDto.BrandId);

        var product = await _productRepository.SelectAll()
            .Where(p => p.PCode.ToUpper() == productForCreationDto.PCode.ToUpper() 
            && p.BarCode == productForCreationDto.BarCode)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product is not null)
        {
            product.Quantity += productForCreationDto.Quantity;
            await _productRepository.UpdateAsync(product);
            throw new CustomException(200, "Bu turdagi mahsulot omborda mavjudligi uchun uning soniga qo'shib qo'yildi.");

        }

        var product1 = await _productRepository.SelectAll()
            .Where(s => s.PCode == productForCreationDto.PCode)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product1 is not null)
            throw new CustomException(409, $"{product1.PCode} - bu kod bazada mavjud.");


        var product2 = await _productRepository.SelectAll()
            .Where(s => s.BarCode == productForCreationDto.BarCode)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product2 is not null)
            throw new CustomException(409, $"{product2.BarCode} - bu kod bazada mavjud.");


        var mappedProduct = _mapper.Map<Product>(productForCreationDto);
        mappedProduct.CreatedAt = DateTime.UtcNow;
        return _mapper.Map<ProductForResultDto>(await _productRepository.InsertAsync(mappedProduct));
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        return await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductForResultDto>> GetAllAsync(PaginationParams @params)
    {
        var products = await _productRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<ProductForResultDto>>(products); 
    }



    public async Task<ProductForResultDto> GetByIdAsync(long id)
    {
        var product = await _productRepository.SelectAll()
             .Where(p => p.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        return _mapper.Map<ProductForResultDto>(product);
    }


    public async Task<ProductForResultDto> UpdateAsync(long id,ProductForUpdateDto productForUpdateDto)
    {
        var category = await _categoryService.RetrieveByIdAsync(productForUpdateDto.CategoryId);

        var brand = await _brandService.RetrieveByIdAsync(productForUpdateDto.BrandId);

        var product = await _productRepository.SelectAll()
             .Where(p => p.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        var mappedProduct = _mapper.Map(productForUpdateDto, product);
        mappedProduct.UpdatedAt = DateTime.UtcNow;

        return _mapper.Map<ProductForResultDto>(await _productRepository.UpdateAsync(mappedProduct));
    }

    public async Task<ProductForResultDto> GetByName(string name)
    {
        var Product =await _productRepository.SelectAll()
            .Where(p => p.ProductName == name)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (Product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        return _mapper.Map<ProductForResultDto>(Product);
    }
}
