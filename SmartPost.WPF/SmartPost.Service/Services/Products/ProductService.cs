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

namespace SmartPost.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryService _categoryService;

    public ProductService(IMapper mapper, IProductRepository productRepository, ICategoryService categoryService, IBrandRepository brandRepository)
    {
        this._mapper = mapper;
        this._brandRepository = brandRepository;
        this._categoryService = categoryService;
        this._productRepository = productRepository;
    }

    public async Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto)
    {
        var category = await _categoryService.RetrieveByIdAsync(productForCreationDto.CategoryId);
           

        if (category is null)
            throw new CustomException(404, "Category is not found");

        var brand = await _brandRepository.SelectAll()
             .Where(b => b.Id == productForCreationDto.BrandId)
             .FirstOrDefaultAsync();

        if (brand is null)
            throw new CustomException(404, "Brand is not found");

        var product = await _productRepository.SelectAll()
            .Where(p => p.ProductName == productForCreationDto.ProductName)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product is not null)
            throw new CustomException(409, "Product is already exists");

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
            throw new CustomException(404, "Product is not found");

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
             .Include(p=>p.Brand)
             .Include(p=>p.Category)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Product is not found");

        return _mapper.Map<ProductForResultDto>(product);
    }


    public async Task<ProductForResultDto> UpdateAsync(long id,ProductForUpdateDto productForUpdateDto)
    {
        var category = await _categoryService.RetrieveByIdAsync(productForUpdateDto.CategoryId);

        if (category is null)
            throw new CustomException(404, "Category is not found");

        var brand = await _brandRepository.SelectAll()
             .Where(b => b.Id == productForUpdateDto.BrandId)
             .FirstOrDefaultAsync();

        if (brand is null)
            throw new CustomException(404, "Brand is not found");

        var product = await _productRepository.SelectAll()
             .Where(p => p.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Product is not found");

        var mappedProduct = _mapper.Map<Product>(productForUpdateDto);
        mappedProduct.Id = id;
        mappedProduct.UpdatedAt = DateTime.UtcNow;

        return _mapper.Map<ProductForResultDto>(await _productRepository.UpdateAsync(mappedProduct));
    }

    public async Task<ProductForResultDto> GetByName(string name)
    {
        var Product =await _productRepository.SelectAll()
            .Where(p=>p.ProductName == name)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (Product is null)
            throw new CustomException(404, "Product is not found");

        return _mapper.Map<ProductForResultDto>(Product);
    }
}
