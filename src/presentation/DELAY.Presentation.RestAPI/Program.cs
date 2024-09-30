using DELAY.Core.Application;
using DELAY.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;

namespace DELAY.Presentation.RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddKeyPerFile(directoryPath: "/run/secrets", optional: true);
            builder.Configuration.AddUserSecrets("a2a5d057-8915-48bc-99fc-5d1869823193");

            builder.Services
                .AddApplication()
                .AddInfrasturcture(builder.Configuration)
                .AddPresentation(builder.Configuration);

            builder.ConfigureWebApplication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // отключение cors
            app.UseCors(x =>
            {
                x.AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("https://localhost", "https://localhost:443", "https://localhost:8043")
                .AllowCredentials();
            });

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
