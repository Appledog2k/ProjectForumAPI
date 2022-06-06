using Articles.Services.Mail;
using Articles.Services.ServiceSetting;
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
            services.AddOptions();
            var mailsettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsettings);

            // todo : Identity
            services.ConfigureIdentity();

            // todo : add automapper

            services.AddAutoMapper(typeof(Startup));

            // todo : add Controller + Json
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // todo : ConnectString
            services.ConfigureConnectString();

            // todo : JWT
            services.ConfigureJWT(Configuration);

            // todo : services
            services.ConfigureServices();

            // todo : Options Identity
            services.ConfigureIdentityOptions();

            // todo : Configure Swagger
            services.ConfigureSwagger();
        }

        //* This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
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
