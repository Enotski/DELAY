using DELAY.Core.Application;
using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DELAY.Presentation.RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddApplication()
                .AddInfrasturcture(builder.Configuration)
                .AddPresentation();

            builder.ConfigureWebApplication();
            builder.Services.AddHttpClient();
            builder.Services.AddCors();

            var tokensSettings = new TokensSettings();
            builder.Configuration.Bind(TokensSettings.SectionName, tokensSettings);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(tokensSettings.SchemeName, options =>
                {

                    using var provider = builder.Services.BuildServiceProvider();

                    options.TokenValidationParameters = provider.GetRequiredService<ITokensService>().GetTokenValidationParameters();

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = ctx =>
                        {
                            ctx.Request.Cookies.TryGetValue("access_token", out var accessToken);
                            if (!string.IsNullOrEmpty(accessToken))
                                ctx.Token = accessToken;
                            return Task.CompletedTask;
                        }
                    };
                });

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
                .AllowAnyOrigin();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
