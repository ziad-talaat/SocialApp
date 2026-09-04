
using System.Text;
using Application.Behaviors;
using Application.Common.Interfaces;
using Application.Service;
using Domain.Entities;
using Domain.IRepositories;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Options;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.ServicesDependencies
{
    public static class DependencyInjection
    {
       
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {

            JwtOption? jwtOptions = configuration.GetSection(JwtOption.SectionName).Get<JwtOption?>();

            var jwtSection = configuration.GetSection(JwtOption.SectionName);



            services.Configure<JwtOption>(configuration.GetSection(JwtOption.SectionName));
            services.Configure<RefreshTokenOption>(configuration.GetSection(RefreshTokenOption.SectionName));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthanticationService, AuthenticationService>();
            services.AddScoped<IAppDbContext, AppDbContext>();

          
            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            })
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //ValidIssuer = configuration["jwt:issuer"],
                    //ValidAudience = configuration["jwt:audience"],
                    ValidIssuer = jwtOptions?.issuer,
                    ValidAudience = jwtOptions?.audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.key))
                };

                //options.Events = new JwtBearerEvents()
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var token = context.Request.Query["access_token"];
                //        var path = context.HttpContext.Request.Path;
                //        if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("/hubs"))
                //        {
                //            context.Token = token;
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
            });


           

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(typeof(IAssemblyMarker).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly,includeInternalTypes:true);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



             services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),

             sqlOptions =>
             {
                 sqlOptions.EnableRetryOnFailure(
                     maxRetryCount: 5,
                     maxRetryDelay: TimeSpan.FromSeconds(30),
                     errorNumbersToAdd: null);
             }


             ));


            return services;
        }
    }
}
