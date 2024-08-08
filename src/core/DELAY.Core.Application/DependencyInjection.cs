using DELAY.Core.Application.Abstractions.Services;
using DELAY.Core.Application.Abstractions.Services.Base;
using DELAY.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DELAY.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<ICryptographyService, CryptographyService>();

            return services;
        }
    }
}
