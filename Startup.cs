using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Articles.Data;
using Articles.Repository;
using Microsoft.OpenApi.Models;
using Articles.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Articles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddRazorPages();

            // add automapper
            services.AddAutoMapper(typeof(Startup));

            // add Controller and Json --- partially update items
            services.AddControllers().AddNewtonsoftJson();

            // add connectString
            services.AddDbContext<ArticleContext>(options =>
            {
                string connectString = Configuration.GetConnectionString("ArticleContext");
                options.UseSqlServer(connectString);
            });






            // add Identity
            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ArticleContext>()
            .AddDefaultTokenProviders();
            // add Authentication
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
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                   };
               });

            // add services
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            // add swagger
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Add Swagger Articles", Version = "v1" });
            // });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}