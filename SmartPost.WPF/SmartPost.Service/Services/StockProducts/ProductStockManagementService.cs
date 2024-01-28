using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Service.Commons.Exceptions;

namespace SmartPost.Service.Services.StockProducts;

public class ProductStockManagementService
{
    private readonly IProductRepository _productRepository;
    private readonly IStockProductRepository _stockProductRepository;

    // Constructor to inject dependencies
    public ProductStockManagementService(IProductRepository productRepository, IStockProductRepository stockProductRepository)
    {
        _productRepository = productRepository;
        _stockProductRepository = stockProductRepository;
    }

    // Method to move products from Product to StockProduct
    public async Task<bool> MoveProductsToStockAsync(IEnumerable<long> productIds, long userId, decimal customQuantityToMove)
    {
        // Fetch products from the database based on provided product IDs
        var products = await _productRepository.SelectAll()
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        // Check if any valid products are found
        if (products.Count == 0)
        {
            throw new CustomException(404, "Mahsulotlarni kiritmadingiz.");
        }

        // List to store StockProducts that will be added to the database
        var stockProductsToAdd = new List<StokProduct>();

        // Loop through each product to prepare StockProduct entities
        foreach (var product in products)
        {
            // Skip products with zero quantity
            if (product.Quantity == 0)
            {
                continue;
            }

            // Determine the quantity to move based on user input 
            decimal quantityToMove = customQuantityToMove;

            if (quantityToMove <= 0)
                throw new CustomException(400, "Mahsulotning hajmini kiriting");

            // Create a new StockProduct entity with relevant information
            var stockProduct = new StokProduct
            {
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                UserId = userId,
                PCode = product.PCode,
                BarCode = product.BarCode,
                ProductName = product.ProductName,
                Size = product.Size,
                Price = product.Price,
                Quantity = quantityToMove,
                Status = "Kutilmoqda", // Set the status to "Kutilmoqda" during accumulation
                CreatedAt = DateTime.UtcNow
            };

            // Check if the requested quantity exceeds the available quantity in the product
            if (quantityToMove > product.Quantity)
                throw new CustomException(400, $"Buncha mahsulot omborhonada mavjud emas.\nSizda {product.Quantity} ta mahsulot mavjud");

            // Deduct the moved quantity from the product's quantity
            product.Quantity -= quantityToMove;

            // Add the StockProduct to the list for later insertion
            stockProductsToAdd.Add(stockProduct);
        }

        // Update the quantities of all products in the database
        foreach (var product in products)
        {
            await _productRepository.UpdateAsync(product);
        }

        // Insert all StockProducts into the StockProduct table
        foreach (var stockProduct in stockProductsToAdd)
        {
            await _stockProductRepository.InsertAsync(stockProduct);

            // After insertion, update the status to "Qo'shildi"
            stockProduct.Status = "Qo'shildi";
            await _stockProductRepository.UpdateAsync(stockProduct);
        }

        // Indicate that the process was successful
        return true;
    }

    public async Task<IEnumerable<StokProduct>> RetrieveAllWithDateTimeAsync(DateTime startDate, DateTime endDate)
    {
        // Use asynchronous method to retrieve data from the database
        var products = await _stockProductRepository.SelectAll()
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .ToListAsync();

        // Return the list of stock products within the specified date range
        return products;
    }

}



