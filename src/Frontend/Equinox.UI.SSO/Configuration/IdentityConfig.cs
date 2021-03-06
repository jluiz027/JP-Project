﻿using Equinox.Infra.CrossCutting.Identity.Context;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;
using Equinox.Infra.CrossCutting.IdentityServer.Context;
using Equinox.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.UI.SSO.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SSOConnection");
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<EquinoxContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<IdentityServerContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<UserIdentity, UserIdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
