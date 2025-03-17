using AutoMapper;
using MedPoint.Domain.Entities.Banners;
using MedPoint.Domain.Entities.CartItems;
using MedPoint.Domain.Entities.Catalogs;
using MedPoint.Domain.Entities.Categories;
using MedPoint.Domain.Entities.Favorites;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.OrderDetails;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Entities.Payments;
using MedPoint.Domain.Entities.Users;
using MedPoint.Service.Configurations;
using MedPoint.Service.Dtos.Banners;
using MedPoint.Service.Dtos.CartItems;
using MedPoint.Service.Dtos.Catalogs;
using MedPoint.Service.Dtos.Categories;
using MedPoint.Service.Dtos.Favorites;
using MedPoint.Service.Dtos.Medications;
using MedPoint.Service.Dtos.OrderDetails;
using MedPoint.Service.Dtos.Orders;
using MedPoint.Service.Dtos.Payments;
using MedPoint.Service.Dtos.Users;

namespace MedPoint.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();

        //Catalog
        CreateMap<Catalog, CatalogForCreationDto>().ReverseMap();
        CreateMap<Catalog, CatalogForResultDto>().ReverseMap();
        CreateMap<Catalog, CatalogForUpdateDto>().ReverseMap();

        //Category
        CreateMap<Category, CategoryForCreationDto>().ReverseMap();
        CreateMap<Category, CategoryForResultDto>().ReverseMap();
        CreateMap<Category, CategoryForUpdateDto>().ReverseMap();

        //Medication
        CreateMap<Medication, MedicationForCreationDto>().ReverseMap();
        CreateMap<Medication, MedicationForResultDto>().ReverseMap();
        CreateMap<Medication, MedicationForUpdateDto>().ReverseMap();

        //Order
        CreateMap<Order, OrderForCreationDto>().ReverseMap();
        CreateMap<Order, OrderForResultDto>().ReverseMap();

        //Payment
        CreateMap<Payment, PaymentForCreationDto>().ReverseMap();
        CreateMap<Payment, PaymentForResultDto>().ReverseMap();
        CreateMap<Payment, PaymentForUpdateDto>().ReverseMap();

        //OrderDetail
        CreateMap<OrderDetail, OrderDetailForCreationDto>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailForResultDto>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailForUpdateDto>().ReverseMap();

        //Banner
        CreateMap<Banner, BannerForCreationDto>().ReverseMap();
        CreateMap<Banner, BannerForResultDto>().ReverseMap();
        CreateMap<Banner, BannerForUpdateDto>().ReverseMap();

        //CartItem
        CreateMap<CartItem, CartItemForCreationDto>().ReverseMap();
        CreateMap<CartItem, CartItemForResultDto>().ReverseMap();

        //Favorite
        CreateMap<Favorite, FavoriteForCreationDto>().ReverseMap();
        CreateMap<Favorite, FavoriteForResultDto>().ReverseMap();

    }
}

