using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Implementations;
using HMS.Repository.Interfaces;
using HMS.Service.Implementations;
using HMS.Service.Interfaces;
using HMS.Service.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HMS.Api
{
    public static class ContainerExtensions
    {
        public static void AddControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
        }
        public static void AddOpenApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddOpenApi();
        }
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<ApplicationDbContext>(options => options
                .ConfigureWarnings(warning => warning.Ignore(RelationalEventId.PendingModelChangesWarning))
                .UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));
        }
        public static void AddAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingProfile));
        }
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IGuestRepository, GuestRepository>();
            builder.Services.AddScoped<IHotelRepository, HotelRepository>();
            builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
        }
        public static void AddJwtTokenGenerator(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        }
        public static void AddAuthService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthService>();
        }
        public static void AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
                options.User.RequireUniqueEmail = false;
                //options.User.AllowedUserNameCharacters = "@";
               

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            var secret = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Secret");
            var issuer = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Issuer");
            var audience = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Audience");
            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });

        }

    }
}
