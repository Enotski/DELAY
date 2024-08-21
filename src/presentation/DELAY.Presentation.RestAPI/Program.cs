using DELAY.Core.Application;
using DELAY.Infrastructure;
using DELAY.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

            builder.Services.AddCors();

            var tokensSettings = new TokensSettings();
            builder.Configuration.Bind(TokensSettings.SectionName, tokensSettings);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(tokensSettings.SchemeName, options =>
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokensSettings.Issuer,
                    ValidAudience = tokensSettings.Audience,
                    IssuerSigningKey = JwtGenerator.GetSymmetricSecurityKey(tokensSettings.SecretKey),
                    LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) => expires > DateTime.UtcNow,
                })
                .AddVkontakte(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Vk:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Vk:ClientSecret"];
                })
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
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
