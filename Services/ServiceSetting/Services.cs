using System.Text;
using Articles.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

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



    }
}