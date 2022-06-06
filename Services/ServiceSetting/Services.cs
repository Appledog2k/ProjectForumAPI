using System.Text;
using Articles.Data;
using Articles.GenericRepository.IRepository;
using Articles.GenericRepository.Repository;
using Articles.Models.DTOs;
using Articles.Models.Errors;
using Articles.Models.Validation;
using Articles.Services.DataHandling;
using Articles.Services.Mail;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Articles.Services.ServiceSetting
{
    public static class Services
    {
        public static IConfiguration Configuration { get; }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApiUser, IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = jwtSettings.GetSection("Key").Value;

            services.AddAuthentication(option =>
          {
              option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
              .AddJwtBearer(option =>
              {
                  option.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidateAudience = false,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
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
                                Message = contextFeature.Error.Message
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

        public static void ConfigureConnectString(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                      {
                          string connectString = Configuration.GetConnectionString("DatabaseContext");
                          options.UseSqlServer(connectString);
                      });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ISendMailService, SendMailService>();
        }

        public static void ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                //* : Setting Password
                options.Password.RequireDigit = false; // Not number required
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Articles API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });
            IMvcBuilder builder = services.AddRazorPages();
        }
    }
}
