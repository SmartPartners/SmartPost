using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.Service.DTOs.Products;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.Brands;
using SmartPost.Domain.Entities.Categories;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.Interfaces.Products;
using SmartPost.Service.Interfaces.Categories;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Service.Interfaces.Brands;

namespace SmartPost.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IBrandService _brandService;
    private readonly IProductRepository _productRepository;
    private readonly IStockProductRepository _stockProductRepository;
    private readonly ICategoryService _categoryService;

    public ProductService(IMapper mapper,
                          IProductRepository productRepository,
                           ICategoryService categoryService,
                           IBrandService brandService,
                           IStockProductRepository stockProductRepository)
    {
        this._mapper = mapper;
        this._brandService = brandService;
        this._categoryService = categoryService;
        this._productRepository = productRepository;
        _stockProductRepository = stockProductRepository;
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
            if (product.Price != productForCreationDto.Price)
            {
                product.Price = productForCreationDto.Price;
                await _productRepository.UpdateAsync(product);

                var stok = await _stockProductRepository.SelectAll()
                    .Where(p => p.PCode == product.PCode)
                    .FirstOrDefaultAsync();
                stok.Price = productForCreationDto.Price;
                await _stockProductRepository.UpdateAsync(stok);
            }

            if (product.BarCode != productForCreationDto.BarCode)
            {
                product.BarCode = productForCreationDto.BarCode;
                await _productRepository.UpdateAsync(product);

                var stok = await _stockProductRepository.SelectAll()
                    .Where(p => p.PCode == product.PCode)
                    .FirstOrDefaultAsync();
                stok.BarCode = productForCreationDto.BarCode;
                await _stockProductRepository.UpdateAsync(stok);
            }

            if (product.BrandId != productForCreationDto.BrandId)
            {
                product.BrandId = productForCreationDto.BrandId;
                await _productRepository.UpdateAsync(product);

                var stok = await _stockProductRepository.SelectAll()
                    .Where(p => p.PCode == product.PCode)
                    .FirstOrDefaultAsync();
                stok.BrandId = productForCreationDto.BrandId;
                await _stockProductRepository.UpdateAsync(stok);
            }

            if (product.ProductName != productForCreationDto.ProductName)
            {
                product.ProductName = productForCreationDto.ProductName;
                await _productRepository.UpdateAsync(product);

                var stok = await _stockProductRepository.SelectAll()
                    .Where(p => p.PCode == product.PCode)
                    .FirstOrDefaultAsync();
                stok.ProductName = productForCreationDto.ProductName;
                await _stockProductRepository.UpdateAsync(stok);
            }

            if (product.PCode != productForCreationDto.PCode)
            {
                product.PCode = productForCreationDto.PCode;
                await _productRepository.UpdateAsync(product);

                var stok = await _stockProductRepository.SelectAll()
                    .Where(p => p.BarCode == product.BarCode)
                    .FirstOrDefaultAsync();
                stok.PCode = productForCreationDto.PCode;
                await _stockProductRepository.UpdateAsync(stok);
            }

            if( product.SalePrice != productForCreationDto.SalePrice)
            {
                product.SalePrice = productForCreationDto.SalePrice;
                await _productRepository.UpdateAsync(product);

                var stok = await _stockProductRepository.SelectAll()
                    .Where(p => p.BarCode == product.BarCode)
                    .FirstOrDefaultAsync();
                stok.SalePrice = productForCreationDto.SalePrice;
                await _stockProductRepository.UpdateAsync(stok);
            }

            if (product.PercentageSalePrice != productForCreationDto.PercentageSalePrice)
            {
                product.PercentageSalePrice = productForCreationDto.PercentageSalePrice;
                await _productRepository.UpdateAsync(product);

                var stok = await _stockProductRepository.SelectAll()
                    .Where(p => p.BarCode == product.BarCode)
                    .FirstOrDefaultAsync();
                stok.PercentageSalePrice = productForCreationDto.PercentageSalePrice;
                await _stockProductRepository.UpdateAsync(stok);
            }

            if (product.CategoryId != productForCreationDto.CategoryId)
            {
                product.CategoryId = productForCreationDto.CategoryId;
                await _productRepository.UpdateAsync(product);

                var stok = await _stockProductRepository.SelectAll()
                    .Where(p => p.BarCode == product.BarCode)
                    .FirstOrDefaultAsync();
                stok.CategoryId = productForCreationDto.CategoryId;
                await _stockProductRepository.UpdateAsync(stok);
            }

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

        if (mappedProduct.SalePrice is null && mappedProduct.PercentageSalePrice is not null)
        {
            decimal? newPrice = (mappedProduct.Price / 100) * mappedProduct.PercentageSalePrice;
            decimal? salePrice = mappedProduct.Price + newPrice;
            mappedProduct.SalePrice = salePrice;
        }
        else if (mappedProduct.SalePrice is not null && mappedProduct.PercentageSalePrice is null)
        {
            decimal? percentPrice = ((mappedProduct.SalePrice - mappedProduct.Price) / mappedProduct.Price) * 100;
            mappedProduct.PercentageSalePrice = (short)percentPrice; 
        }


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

    public async Task<ProductForResultDto> InsertWithBarCodeAsync(string barCode, decimal quantity)
    {
        var product = await _productRepository.SelectAll()
             .Where(p => p.BarCode == barCode)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        var mappedProduct = new Product
        {
            Id = product.Id,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            ProductName = product.ProductName,
            BarCode = product.BarCode,
            PCode = product.PCode,
            Price = product.Price,
            SalePrice = product.SalePrice,
            PercentageSalePrice = product.PercentageSalePrice,
            CreatedAt = DateTime.UtcNow
        };
        mappedProduct.Quantity += quantity;
        mappedProduct.UpdatedAt = DateTime.UtcNow;
        await _productRepository.UpdateAsync(mappedProduct);

        return _mapper.Map<ProductForResultDto>(mappedProduct);
    }

    #region
    /*public async Task<ProductForResultDto> UpdateAsync(long id, ProductForUpdateDto productForUpdateDto)
    {
        var category = await _categoryService.RetrieveByIdAsync(productForUpdateDto.CategoryId);

        var brand = await _brandService.RetrieveByIdAsync(productForUpdateDto.BrandId);

        var product = await _productRepository.SelectAll()
             .Where(p => p.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        if (product.Price != productForUpdateDto.Price)
        {
            product.Price = productForUpdateDto.Price;
            await _productRepository.UpdateAsync(product);

            var stok = await _stockProductRepository.SelectAll()
                .Where(p => p.PCode == product.PCode)
                .FirstOrDefaultAsync();
            stok.Price = productForUpdateDto.Price;
            await _stockProductRepository.UpdateAsync(stok);
        }

        if (product.BarCode != productForUpdateDto.BarCode)
        {
            product.BarCode = productForUpdateDto.BarCode;
            await _productRepository.UpdateAsync(product);

            var stok = await _stockProductRepository.SelectAll()
                .Where(p => p.PCode == product.PCode)
                .FirstOrDefaultAsync();
            stok.BarCode = productForUpdateDto.BarCode;
            await _stockProductRepository.UpdateAsync(stok);
        }

        if (product.BrandId != productForUpdateDto.BrandId)
        {
            product.BrandId = productForUpdateDto.BrandId;
            await _productRepository.UpdateAsync(product);

            var stok = await _stockProductRepository.SelectAll()
                .Where(p => p.PCode == product.PCode)
                .FirstOrDefaultAsync();
            stok.BrandId = productForUpdateDto.BrandId;
            await _stockProductRepository.UpdateAsync(stok);
        }

        if (product.ProductName != productForUpdateDto.ProductName)
        {
            product.ProductName = productForUpdateDto.ProductName;
            await _productRepository.UpdateAsync(product);

            var stok = await _stockProductRepository.SelectAll()
                .Where(p => p.PCode == product.PCode)
                .FirstOrDefaultAsync();
            stok.ProductName = productForUpdateDto.ProductName;
            await _stockProductRepository.UpdateAsync(stok);
        }

        if (product.Size != productForUpdateDto.Size)
        {
            product.Size = productForUpdateDto.Size;
            await _productRepository.UpdateAsync(product);

            var stok = await _stockProductRepository.SelectAll()
                .Where(p => p.PCode == product.PCode)
                .FirstOrDefaultAsync();
            stok.Size = productForUpdateDto.Size;
            await _stockProductRepository.UpdateAsync(stok);
        }

        if (product.PCode != productForUpdateDto.PCode)
        {
            product.PCode = productForUpdateDto.PCode;
            await _productRepository.UpdateAsync(product);

            var stok = await _stockProductRepository.SelectAll()
                .Where(p => p.BarCode == product.BarCode)
                .FirstOrDefaultAsync();
            stok.PCode = productForUpdateDto.PCode;
            await _stockProductRepository.UpdateAsync(stok);
        }

        if (product.CategoryId != productForUpdateDto.CategoryId)
        {
            product.CategoryId = productForUpdateDto.CategoryId;
            await _productRepository.UpdateAsync(product);

            var stok = await _stockProductRepository.SelectAll()
                .Where(p => p.BarCode == product.BarCode)
                .FirstOrDefaultAsync();
            stok.CategoryId = productForUpdateDto.CategoryId;
            await _stockProductRepository.UpdateAsync(stok);
        }

        if (product.Quantity != productForUpdateDto.Quantity)
        {
            product.Quantity += productForUpdateDto.Quantity;
            await _productRepository.UpdateAsync(product);
        }

        var mappedProduct = _mapper.Map(productForUpdateDto, product);
        mappedProduct.UpdatedAt = DateTime.UtcNow;

        var result = await _productRepository.UpdateAsync(mappedProduct);

        return _mapper.Map<ProductForResultDto>(result);
    }*/

    #endregion

    public async Task<ProductForResultDto> UpdateAsync(long id, ProductForUpdateDto productForUpdateDto)
    {
        var category = await _categoryService.RetrieveByIdAsync(productForUpdateDto.CategoryId);
        var brand = await _brandService.RetrieveByIdAsync(productForUpdateDto.BrandId);

        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        if (product.Price != productForUpdateDto.Price)
            product.Price = productForUpdateDto.Price;

        if (product.BarCode != productForUpdateDto.BarCode)
            product.BarCode = productForUpdateDto.BarCode;

        if (product.BrandId != productForUpdateDto.BrandId)
            product.BrandId = productForUpdateDto.BrandId;

        if (product.ProductName != productForUpdateDto.ProductName)
            product.ProductName = productForUpdateDto.ProductName;

        if (product.PCode != productForUpdateDto.PCode)
            product.PCode = productForUpdateDto.PCode;


        if (product.CategoryId != productForUpdateDto.CategoryId)
            product.CategoryId = productForUpdateDto.CategoryId;

        if (product.Quantity != productForUpdateDto.Quantity)
            product.Quantity += productForUpdateDto.Quantity;

        if (product.SalePrice != productForUpdateDto.SalePrice)
            product.SalePrice = productForUpdateDto.SalePrice;

        if (product.PercentageSalePrice != productForUpdateDto.PercentageSalePrice)
            product.PercentageSalePrice = productForUpdateDto.PercentageSalePrice;

        if (product.SalePrice is null && product.PercentageSalePrice is not null)
        {
            decimal? newPrice = (product.Price / 100) * product.PercentageSalePrice;
            decimal? salePrice = product.Price + newPrice;
            product.SalePrice = salePrice;
        }
        else if (product.SalePrice is not null && product.PercentageSalePrice is null)
        {
            decimal? percentPrice = ((product.Price - product.SalePrice) / product.Price) * 100;
            decimal? percentSale = 100 - percentPrice;
            product.PercentageSalePrice = (short?)percentSale;
        }

        await _productRepository.UpdateAsync(product);

        var stockProduct = await _stockProductRepository.SelectAll()
            .Where(p => p.PCode == product.PCode)
            .FirstOrDefaultAsync();

        if (stockProduct != null)
        {
            stockProduct.Price = productForUpdateDto.Price;
            stockProduct.SalePrice = productForUpdateDto.SalePrice;
            stockProduct.PercentageSalePrice = productForUpdateDto.PercentageSalePrice;
            stockProduct.BarCode = productForUpdateDto.BarCode;
            stockProduct.BrandId = productForUpdateDto.BrandId;
            stockProduct.ProductName = productForUpdateDto.ProductName;
            stockProduct.PCode = productForUpdateDto.PCode;
            stockProduct.CategoryId = productForUpdateDto.CategoryId;

            await _stockProductRepository.UpdateAsync(stockProduct);
        }

        var mappedProduct = _mapper.Map<ProductForResultDto>(product);
        return mappedProduct;
    }


    public async Task<ProductForResultDto> GetByName(string name)
    {
        var Product = await _productRepository.SelectAll()
            .Where(p => p.ProductName == name)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (Product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        return _mapper.Map<ProductForResultDto>(Product);
    }
}
