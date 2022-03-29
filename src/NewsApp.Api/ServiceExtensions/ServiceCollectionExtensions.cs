using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NewsApp.Api.Middleware.Models;
using NewsApp.Infrastructure.Models;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;
using System.Text;

namespace NewsApp.Api.ServiceExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMongoDB(this IServiceCollection services)
        {
            services.AddSingleton<MongoDBContext>();
        }

        public static void AddJWTToken(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            JwtTokenConfig jwtTokenConfig = new JwtTokenConfig();
            jwtTokenConfig.Issuer = configuration.GetValue<string>("jwtTokenConfig:issuer");
            jwtTokenConfig.Secret = configuration.GetValue<string>("jwtTokenConfig:secret");
            jwtTokenConfig.Audience = configuration.GetValue<string>("jwtTokenConfig:audience");
            jwtTokenConfig.AccessTokenExpiration = configuration.GetValue<int>("jwtTokenConfig:accessTokenExpiration");
            jwtTokenConfig.RefreshTokenExpiration = configuration.GetValue<int>("jwtTokenConfig:refreshTokenExpiration");

            services.AddSingleton(jwtTokenConfig);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });
        }

        public static void AddRedis(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            var conf = new RedisConfiguration
            {
                ConnectionString = configuration.GetConnectionString("Redis"),
                ServerEnumerationStrategy = new ServerEnumerationStrategy
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
                }
            };

            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(conf);
        }
    }
}
