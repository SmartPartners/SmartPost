using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.Service.DTOs.Products;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.Barnds;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.DataAccess.Interfaces.Categories;

namespace SmartPost.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IMapper mapper, IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
    {
        this._mapper = mapper;
        this._productRepository = productRepository;
        this._categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
    }

    public async Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto)
    {
        var category = await _categoryRepository.SelectAll()
           .Where(c => c.Id == productForCreationDto.CategoryId)
           .AsNoTracking()
           .FirstOrDefaultAsync();

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


        if (product is null )
        {
            var mappedProduct = _mapper.Map<Product>(productForCreationDto);
            mappedProduct.CreatedAt = DateTime.UtcNow;
            return _mapper.Map<ProductForResultDto>(await _productRepository.InsertAsync(mappedProduct));
        }

        product.Quantity += productForCreationDto.Quantity;
        return _mapper.Map<ProductForResultDto>(await _productRepository.UpdateAsync(product));

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

    public async Task<IEnumerable<ProductForResultDto>> GetAllAsync()
       => _mapper.Map<IEnumerable<ProductForResultDto>>(await _productRepository.SelectAll().AsNoTracking().ToListAsync());



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


    public async Task<ProductForResultDto> UpdateAsync(ProductForUpdateDto productForUpdateDto)
    {
        var category = await _categoryRepository.SelectAll()
           .Where(c => c.Id == productForUpdateDto.CategoryId)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (category is null)
            throw new CustomException(404, "Category is not found");

        var brand = await _brandRepository.SelectAll()
             .Where(b => b.Id == productForUpdateDto.BrandId)
             .FirstOrDefaultAsync();

        if (brand is null)
            throw new CustomException(404, "Brand is not found");

        var product = await _productRepository.SelectAll()
             .Where(p => p.Id == productForUpdateDto.Id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Product is not found");

        var mappedProduct = _mapper.Map<Product>(productForUpdateDto);
        mappedProduct.UpdatedAt = DateTime.UtcNow;

        return _mapper.Map<ProductForResultDto>(await _productRepository.UpdateAsync(mappedProduct));
    }

    public async Task<ProductForResultDto> GetByName(string name)
    {
        var Product =await _productRepository.SelectAll()
            .Where(p=>p.ProductName == name)
            .Include(s=>s.Brand)
            .Include(s=>s.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (Product is null)
            throw new CustomException(404, "Product is not found");

        return _mapper.Map<ProductForResultDto>(Product);
    }
}
