using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
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
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {            
            Configuration = configuration;
        }

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


            //local DB
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("LocalDBConnection")));

            //prod DB
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("ProductionDBConnection"));
            builder.Password = Configuration["ProdDbPassword"]; ///add pass to connenstionstring from secrets.json      
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.ConnectionString));

            //only for test
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("ProductionDBConnection")));


            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //info:
            //services.AddTransient - created each time they're requested from the service container. This lifetime works best for lightweight, stateless services
            //services.AddScoped - once per client request (connection)
            //services.AddSingleton - created the first time they're requested

            // repositories
            services.AddScoped<IBookRepository, BookRepository>(); 
            services.AddScoped<IUserDataRepository, UserDataRepository>();

            // services
            services.AddScoped<IBookContentService, BookContentService>();
            services.AddScoped<ITypingServices, TypingService>();            
            services.AddMvc().AddNewtonsoftJson();
            services.AddMemoryCache(); // IMemoryCache store data in the memory of web server, in future set size/limit to cache => https://docs.microsoft.com/pl-pl/aspnet/core/performance/caching/memory?view=aspnetcore-2.2#use-setsize-size-and-sizelimit-to-limit-cache-size

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            //add for tests
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseHsts();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

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
