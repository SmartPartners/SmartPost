using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Barnds;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.Brands;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.DTOs.Brands;
using SmartPost.Service.Interfaces.Brands;

namespace SmartPost.Service.Services.Brands
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandForResultDto> CreateAsync(BrandForCreationDto dto)
        {
            var brand = await _brandRepository.SelectAll()
                .Where(c => c.Name.ToLower() == dto.Name.ToLower())
                .FirstOrDefaultAsync();
            if (brand is not null)
                throw new CustomException(403, "Ushbu brend bazada mavjud.");

            var mapped = _mapper.Map<Brand>(dto);
            mapped.CreatedAt = DateTime.UtcNow;

            var result = await _brandRepository.InsertAsync(mapped);

            return _mapper.Map<BrandForResultDto>(result);
        }

        public async Task<BrandForResultDto> ModifyAsync(long id, BrandForUpdateDto dto)
        {
            var brand = await _brandRepository.SelectAll()
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (brand is null)
                throw new CustomException(404, "Brend topilmadi.");

            var mapperBrand = _mapper.Map(dto, brand);
            mapperBrand.CreatedAt = DateTime.UtcNow;

            var result = await _brandRepository.UpdateAsync(mapperBrand);

            return _mapper.Map<BrandForResultDto>(result);
        }

        public async Task<bool> ReamoveAsync(long id)
        {
            var brand = await _brandRepository.SelectAll()
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (brand is null)
                throw new CustomException(404, "Brend topilmadi.");

            await _brandRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<BrandForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var brand = await _brandRepository.SelectAll()
                .Include(c => c.Products)
                .Include(c => c.StokProducts)
                .Include(c => c.InventoryLists)
                .Include(c =>c.PartnersProducts)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BrandForResultDto>>(brand);
        }

        public async Task<BrandForResultDto> RetrieveByIdAsync(long id)
        {
            var brand = await _brandRepository.SelectAll()
                .Where(c => c.Id == id)
                 .Include(c => c.Products)
                 .Include(c => c.StokProducts)
                 .Include(c => c.InventoryLists)
                 .Include(c => c.PartnersProducts)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();
            if (brand is null)
                throw new CustomException(404, "Brend topilmadi.");

            return _mapper.Map<BrandForResultDto>(brand);
        }
    }
}
