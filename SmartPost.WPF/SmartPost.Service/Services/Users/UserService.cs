using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.Users;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.Users;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.Commons.Helpers;
using SmartPost.Service.DTOs.Users;
using SmartPost.Service.Interfaces.Users;

namespace SmartPost.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }


    public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.PhoneNumber == dto.PhoneNumber)
            .FirstOrDefaultAsync();
        if (user is not null)
            throw new CustomException(403, "User is already exists");

        var hasherResult = PasswordHelper.Hash(dto.Password);
        var mapped = _mapper.Map<User>(dto);

        mapped.CreatedAt = DateTime.UtcNow;
        mapped.PasswordSalt = hasherResult.Salt;
        mapped.Password = hasherResult.Hash;

        var result = await _userRepository.InsertAsync(mapped);
        return _mapper.Map<UserForResultDto>(result);
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
             .Where(u => u.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "User is not found!");

        var mapped = _mapper.Map(dto, user);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(mapped);

        return _mapper.Map<UserForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _userRepository.SelectAll()
              .Where(u => u.Id == id)
              .AsNoTracking()
              .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "User is not found!");

        await _userRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = await _userRepository.SelectAll()
             .Include(u => u.StokProducts)
             .Include(uc => uc.Cards)
             .AsNoTracking()
             .ToPagedList(@params)
             .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var user = await _userRepository.SelectAll()
             .Where(u => u.Id == id)
             .Include(u => u.StokProducts)
             .Include(uc => uc.Cards)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "User is not found!");

        return _mapper.Map<UserForResultDto>(user);
    }

    public async Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (user is null || !PasswordHelper.Verify(dto.OldPassword, user.PasswordSalt, user.Password))
            throw new CustomException(404, "User or Password is incorrect");
        else if (dto.NewPassword != dto.ConfirmPassword)
            throw new CustomException(400, "New password and confir password aren't equal");

        var hash = PasswordHelper.Hash(dto.ConfirmPassword);
        user.PasswordSalt = hash.Salt;
        user.Password = hash.Hash;

        await _userRepository.UpdateAsync(user);

        return true;
    }
}
