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
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // use swagger
            // app.UseSwagger();
            // app.UseSwaggerUI(c =>
            // {
            //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Add Swagger Articles");
            // });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapRazorPages();
            });
        }
    }
}