using DELAY.Core.Application;
using DELAY.Infrastructure.DependencyInjections;
using Microsoft.AspNetCore.CookiePolicy;

namespace DELAY.Presentation.RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddPresentation(builder.Configuration);

            builder.ConfigureWebApplication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ���������� cors
            app.UseCors(x =>
            {
                x.AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("https://localhost", "https://localhost:443") // ���� � ������ SPA �������
                .AllowCredentials();
            });

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
