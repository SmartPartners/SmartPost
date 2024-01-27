using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.InventoryLists;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.InventoryLists;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.DTOs.InventoryLists;
using SmartPost.Service.Interfaces.Brands;
using SmartPost.Service.Interfaces.Categories;
using SmartPost.Service.Interfaces.InventoryLists;

namespace SmartPost.Service.Services.InventoryLists;

public class InventoryListService : IInventoryListService
{
    private readonly IInventoryListRepository _inventoryListRepository;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly IMapper _mapper;

    public InventoryListService(
        IInventoryListRepository inventoryListRepository,
        IMapper mapper,
        ICategoryService categoryService,
        IBrandService brandService)
    {
        _mapper = mapper;
        _inventoryListRepository = inventoryListRepository;
        _categoryService = categoryService;
        _brandService = brandService;
    }

    public async Task<InventoryListForResultDto> CreateAsync(InventoryListForCreationDto dto)
    {
        var brand = await _brandService.RetrieveByIdAsync(dto.BrandId);
        var category = await _categoryService.RetrieveByIdAsync(dto.CategoryId);

        var existingInventory = await _inventoryListRepository.SelectAll()
            .Where(i => i.ProductName == dto.ProductName)
            .FirstOrDefaultAsync();

        if (existingInventory != null)
        {
            existingInventory.Quantity += dto.Quantity;
            await _inventoryListRepository.UpdateAsync(existingInventory);

            return _mapper.Map<InventoryListForResultDto>(existingInventory);
        }

        var mapped = _mapper.Map<InventoryList>(dto);
        mapped.CreatedAt = DateTime.UtcNow;

        var result = await _inventoryListRepository.InsertAsync(mapped);

        return _mapper.Map<InventoryListForResultDto>(result);
    }


    public async Task<InventoryListForResultDto> ModifyAsync(long id, InventoryListForUpdateDto dto)
    {
        var inventory = await _inventoryListRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (inventory is null)
            throw new CustomException(404, "Inventory list is not found");

        var brand = await _brandService.RetrieveByIdAsync(dto.BrandId);
        var category = await _categoryService.RetrieveByIdAsync(dto.CategoryId);

        var mapped = _mapper.Map(dto, inventory);
        mapped.UpdatedAt = DateTime.UtcNow;

        var result = await _inventoryListRepository.UpdateAsync(mapped);

        return _mapper.Map<InventoryListForResultDto>(result);
    }

    public async Task<bool> ReamoveAsync(long id)
    {
        var inventory = await _inventoryListRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (inventory is null)
            throw new CustomException(404, "Inventory list is not found");

        await _inventoryListRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<InventoryListForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var inventories = await _inventoryListRepository.SelectAll()
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();

        return _mapper.Map<IEnumerable<InventoryListForResultDto>>(inventories);
    }

    public async Task<InventoryListForResultDto> RetrieveByIdAsync(long id)
    {
        var inventory = await _inventoryListRepository.SelectAll()
                .Where(c => c.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        if (inventory is null)
            throw new CustomException(404, "Inventory list is not found");

        return _mapper.Map<InventoryListForResultDto>(inventory);
    }
}
