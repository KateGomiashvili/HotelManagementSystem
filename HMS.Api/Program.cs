using HMS.Api.Middleware;
using HMS.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.AddDatabase();
            builder.AddAutoMapper();
            builder.AddRepositories();
            builder.AddIdentity();
            builder.AddAuthentication();
            builder.AddJwtTokenGenerator();
            builder.AddServices();
            builder.AddAuthService();

            var app = builder.Build();

            app.CreateDatabaseAutomatically();
            app.UseStaticFiles();
            app.UseMiddleware<ExceptionHandlingMidlleware>();

            app.MapOpenApi();
            //app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
