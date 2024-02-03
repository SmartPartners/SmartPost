using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Partners;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.Partners;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.DTOs.Partners;
using SmartPost.Service.Interfaces.Partners;

namespace SmartPost.Service.Services.Partners;

public class PartnerService : IPartnerService
{
    private readonly IPartnerRepository _partnerRepository;
    private readonly IMapper _mapper;

    public PartnerService(IPartnerRepository partnerRepository, IMapper mapper)
    {
        _partnerRepository = partnerRepository;
        _mapper = mapper;
    }

    public async Task<PartnerForResultDto> CreateAsync(PartnerForCreationDto dto)
    {
        var user = await _partnerRepository.SelectAll()
            .Where(u => u.PhoneNumber == dto.PhoneNumber)
            .FirstOrDefaultAsync();

        if (user is not null)
            throw new CustomException(403, "Bu hamkorimiz mavjud.");

        var mapped = _mapper.Map<Partner>(dto);
        mapped.CreatedAt = DateTime.UtcNow;

        var result = await _partnerRepository.InsertAsync(mapped);

        return _mapper.Map<PartnerForResultDto>(result);
    }

    public async Task<PartnerForResultDto> ModifyAsync(long id, PartnerForUpdateDto dto)
    {
        var user = await _partnerRepository.SelectAll()
             .Where(u => u.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "Hamkor topilmadi.");

        var mapped = _mapper.Map(dto, user);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _partnerRepository.UpdateAsync(mapped);

        return _mapper.Map<PartnerForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _partnerRepository.SelectAll()
              .Where(u => u.Id == id)
              .AsNoTracking()
              .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "Hamkor topilmadi.");

        await _partnerRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<PartnerForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = await _partnerRepository.SelectAll()
             .Include(u => u.PartnersProducts)
             .AsNoTracking()
             .ToPagedList(@params)
             .ToListAsync();

        return _mapper.Map<IEnumerable<PartnerForResultDto>>(users);
    }

    public async Task<PartnerForResultDto> RetrieveByIdAsync(long id)
    {
        var user = await _partnerRepository.SelectAll()
             .Where(u => u.Id == id)
             .Include(u => u.PartnersProducts)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "Hamkor topilmadi.");

        return _mapper.Map<PartnerForResultDto>(user);
    }
}
