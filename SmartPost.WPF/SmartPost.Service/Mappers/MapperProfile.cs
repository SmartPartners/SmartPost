using AutoMapper;
using SmartPost.Domain.Entities.Cards;
using SmartPost.Domain.Entities.Categories;
using SmartPost.Domain.Entities.Users;
using SmartPost.Service.DTOs.Cards;
using SmartPost.Service.DTOs.Categories;
using SmartPost.Service.DTOs.Users;

namespace SmartPost.Service.Mappers;
/// <summary>
/// Keyingi bosqich Service Interface ni yaratish
/// </summary>
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Users
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForChangePasswordDto>().ReverseMap();

        // Category
        CreateMap<Category, CategoryForResultDto>().ReverseMap();
        CreateMap<Category, CategoryForUpdateDto>().ReverseMap();
        CreateMap<Category, CategoryForCreationDto>().ReverseMap();

        // Cards
        CreateMap<Card, CardForResultDto>().ReverseMap();
        CreateMap<Card, CardForUpdateDto>().ReverseMap();
        CreateMap<Card, CardForCreationDto>().ReverseMap();
    }
}
