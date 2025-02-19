using FluentValidation;
using MedPoint.Data.IRepositories;
using MedPoint.Data.Repositories;
using MedPoint.Domain.Entities.Banners;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Users;
using MedPoint.Domain.Validations;
using MedPoint.Service.Dtos.Users;
using MedPoint.Service.Interfaces.IAuthServices;
using MedPoint.Service.Interfaces.IBannerServices;
using MedPoint.Service.Interfaces.ICartItemServices;
using MedPoint.Service.Interfaces.ICatalogServices;
using MedPoint.Service.Interfaces.ICategoryServices;
using MedPoint.Service.Interfaces.IFavoriteServices;
using MedPoint.Service.Interfaces.IMedicationServices;
using MedPoint.Service.Interfaces.IOrderServices;
using MedPoint.Service.Interfaces.IPaymentServices;
using MedPoint.Service.Interfaces.IUserServices;
using MedPoint.Service.Interfaces.OrderDetailServices;
using MedPoint.Service.Mappers;
using MedPoint.Service.Services.AuthServices;
using MedPoint.Service.Services.BannerServices;
using MedPoint.Service.Services.CartItemServices;
using MedPoint.Service.Services.CatalogServices;
using MedPoint.Service.Services.CategoryServices;
using MedPoint.Service.Services.FavoriteServices;
using MedPoint.Service.Services.MedicationServices;
using MedPoint.Service.Services.OrderDetailServices;
using MedPoint.Service.Services.OrderServices;
using MedPoint.Service.Services.PaymentServices;
using MedPoint.Service.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace MedPoint.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCustomService(this IServiceCollection services)
        {

            //Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //UserService
            services.AddScoped<IUserService, UserService>();

            //AuthService
            services.AddScoped<IAuthService, AuthService>();

            //UserValidation
            services.AddTransient<IValidator<UserForCreationDto>, UserValidation>();

            //CatalogService
            services.AddScoped<ICatalogService, CatalogService>();

            //CategoryService
            services.AddScoped<ICategoryService, CategoryService>();

            //MedicationService
            services.AddScoped<IMedicationService, MedicationService>();

            //MedicationService
            services.AddScoped<MedicationService>();

            //OrderService
            services.AddScoped<IOrderService, OrderService>();

            //Payment
            services.AddScoped<IPaymentService, PaymentService>();

            //OrderDetailService
            services.AddScoped<IOrderDetailService, OrderDetailService>();

            //Banner
            services.AddScoped<IBannerService, BannerService>();

            //CartItem
            services.AddScoped<ICartItemService, CartItemService>();

            //Favorite
            services.AddScoped<IFavoriteService, FavoriteService>();

            //Mapping
            services.AddAutoMapper(typeof(MappingProfile));

            //Logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
        }
        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedPoint.Api", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });
        }
    }
}
