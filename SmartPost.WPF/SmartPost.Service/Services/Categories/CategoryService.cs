using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Categories;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.Categories;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.DTOs.Categories;
using SmartPost.Service.Interfaces.Categories;

namespace SmartPost.Service.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryForResultDto> CreateAsync(CategoryForCreationDto dto)
    {
        var category = await _categoryRepository.SelectAll()
            .Where(c => c.Name == dto.Name)
            .FirstOrDefaultAsync();
        if (category is not null)
            throw new CustomException(403, "Category is already exists");

        var mappedCategory = _mapper.Map<Category>(dto);
        mappedCategory.CreatedAt = DateTime.UtcNow;

        var result = await _categoryRepository.InsertAsync(mappedCategory);

        return _mapper.Map<CategoryForResultDto>(result);
    }

    public async Task<CategoryForResultDto> ModifyAsync(long id, CategoryForUpdateDto dto)
    {
        var category = await _categoryRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(404, "Category is not found");

        var mappedCategory = _mapper.Map(dto, category);
        mappedCategory.UpdatedAt = DateTime.UtcNow;

        var result = await _categoryRepository.UpdateAsync(mappedCategory);

        return _mapper.Map<CategoryForResultDto>(result);
    }

    public async Task<bool> ReamoveAsync(long id)
    {
        var category = await _categoryRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(404, "Category is not found");

        await _categoryRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var categories = await _categoryRepository.SelectAll()
                .Include(c => c.Products)
                .Include(c => c.StokProducts)
                .Include(c => c.InventoryLists)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();

        return _mapper.Map<IEnumerable<CategoryForResultDto>>(categories);
    }

    public async Task<CategoryForResultDto> RetrieveByIdAsync(long id)
    {
        var category = await _categoryRepository.SelectAll()
                .Where(c => c.Id == id)
                .Include(c => c.Products)
                .Include(c => c.StokProducts)
                .Include(c => c.InventoryLists)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(404, "Category is not found");

        return _mapper.Map<CategoryForResultDto>(category);
    }


}
