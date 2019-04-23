using System;
using Core.Services;
using Core.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace NetCoreWithJWT
{
    public class Startup
    {
        string sqlConnectionString = string.Empty;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string jwtKey = Configuration["Authentication:JwtKey"];

            services.AddScoped<ITokenService, TokenService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, //Gets or sets a boolean to control if the issuer will be validated during token validation.
                    ValidateAudience = false,//Gets or sets a boolean to control if the audience will be validated during token validation.
                    ValidateLifetime = true,//Gets or sets a boolean to control if the lifetime will be validated during token validation.
                    ValidateIssuerSigningKey = true,//Gets or sets a boolean that controls if validation of the SecurityKey that signed the securityToken is called.
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtKey)),//Gets or sets the SecurityKey that is to be used for signature validation.
                    ClockSkew = TimeSpan.FromSeconds(5)//Gets or sets the clock skew to apply when validating a time.
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
