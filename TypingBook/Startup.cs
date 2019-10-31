using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TypingBook.Data;
using TypingBook.Repositories;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services;
using TypingBook.Services.IServices;

namespace TypingBook
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                // custom passoward validation
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
                        
            services.AddIdentity<IdentityUser, IdentityRole>()
                //.AddDefaultUI(UIFramework.Bootstrap4) //changed after update to core 3.0 (from preview version)
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // repositories
            services.AddScoped<IBookRepository, BookRepository>(); 
            services.AddScoped<IAgreementRepository, AgreementRepository>();
            services.AddScoped<IUserDataRepository, UserDataRepository>();

            // services
            services.AddScoped<ITypingServices, TypingServices>();            
            services.AddMvc().AddNewtonsoftJson();
            services.AddMemoryCache(); // IMemoryCache store data in the memory of web server, in future set size/limit to cache => https://docs.microsoft.com/pl-pl/aspnet/core/performance/caching/memory?view=aspnetcore-2.2#use-setsize-size-and-sizelimit-to-limit-cache-size

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapRazorPages();
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Typing}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();
        }
    }
}
