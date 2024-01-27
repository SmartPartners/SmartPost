using AutoMapper;
using SmartPost.Domain.Entities.Brands;
using SmartPost.Domain.Entities.CancelOrders;
using SmartPost.Domain.Entities.Cards;
using SmartPost.Domain.Entities.Categories;
using SmartPost.Domain.Entities.InventoryLists;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.Domain.Entities.Users;
using SmartPost.Service.DTOs.Brands;
using SmartPost.Service.DTOs.CancelOrders;
using SmartPost.Service.DTOs.Cards;
using SmartPost.Service.DTOs.Categories;
using SmartPost.Service.DTOs.InventoryLists;
using SmartPost.Service.DTOs.Products;
using SmartPost.Service.DTOs.StockProducts;
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

        // Brand
        CreateMap<Brand, BrandForCreationDto>().ReverseMap();
        CreateMap<Brand, BrandForResultDto>().ReverseMap();
        CreateMap<Brand, BrandForUpdateDto>().ReverseMap();

        // Cards
        CreateMap<Card, CardForResultDto>().ReverseMap();
        CreateMap<Card, CardForUpdateDto>().ReverseMap();
        CreateMap<Card, CardForCreationDto>().ReverseMap();

        // CancelOrder
        CreateMap<CancelOrder, CancelOrderForResultDto>().ReverseMap();
        CreateMap<CancelOrder, CancelOrderForUpdateDto>().ReverseMap();
        CreateMap<CancelOrder, CancelOrderForCreationDto>().ReverseMap();

        // InventoryList
        CreateMap<InventoryList, InventoryListForResultDto>().ReverseMap();
        CreateMap<InventoryList, InventoryListForUpdateDto>().ReverseMap();
        CreateMap<InventoryList, InventoryListForCreationDto>().ReverseMap();

        //Products
        CreateMap<Product, ProductForResultDto>().ReverseMap();
        CreateMap<Product, ProductForUpdateDto>().ReverseMap();
        CreateMap<Product, ProductForCreationDto>().ReverseMap();

        //StockProducts
        CreateMap<StokProduct, StockProductForUpdateDto>().ReverseMap();
        CreateMap<StokProduct, StockProductsForResultDto>().ReverseMap();
        CreateMap<StokProduct, StockProductForCreationDto>().ReverseMap();
    }
}
