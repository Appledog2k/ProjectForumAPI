using System.Text;
using Articles.Data;
using Articles.Models.DTOs;
using Articles.Models.Errors;
using Articles.Models.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Articles.Services.ServiceSetting
{
    public static class Services
    {
        // TODO: Configure Identity

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApiUser, IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
        }

        public static void JWT(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(option =>
          {
              option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
          })
              .AddJwtBearer(option =>
              {
                  option.SaveToken = true;
                  option.RequireHttpsMetadata = false;
                  option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidAudience = Configuration["JWT:ValidAudience"],
                      ValidIssuer = Configuration["JWT:ValidIssuer"],
                      RequireExpirationTime = true, //time deadline
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),// create symmetric key
                      ValidateIssuerSigningKey = true
                  };
              });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                error =>
                {
                    error.Run(async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            Log.Error($"Something Went Wrong in the {contextFeature.Error}");

                            await context.Response.WriteAsync(new Error
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error. Please Try Again Later.",
                            }.ToString());
                        }
                    });
                }
            );
        }

        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UserDTO>, UserValidation>();
            services.AddTransient<IValidator<Create_AuthorDTO>, AuthorValidation>();
            services.AddTransient<IValidator<Create_ArticleDTO>, ArticleValidation>();
        }
    }
}