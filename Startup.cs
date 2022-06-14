using Articles.Data;
using Articles.Services.Mail;
using Articles.Services.ServiceSetting;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Articles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // todo : Add Razor pages
            services.AddRazorPages();

            // todo : Mail
            services.ConfigureEmailService(Configuration);

            // todo : Identity
            services.ConfigureIdentity();

            // todo : add automapper

            services.AddAutoMapper(typeof(Startup));

            // todo : add Controller + Json
            services.AddControllers().AddNewtonsoftJson(op =>
            op.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddFluentValidation();

            // todo : ConnectString

            services.AddDbContext<DatabaseContext>(options =>
                      {
                          string connectString = Configuration.GetConnectionString("DatabaseContext");
                          options.UseSqlServer(connectString);
                      });

            // todo : JWT
            services.ConfigureJWT(Configuration);

            // todo : services
            services.ConfigureServices();

            // todo : Options Identity
            services.ConfigureIdentityOptions();

            // todo : Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Article", Version = "v1" });
            });

            // todo : Configure FluentValidation
            services.ConfigureValidation();




        }

        //* This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Article v1"));

            app.UseHttpsRedirection();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.ConfigureExceptionHandler();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
