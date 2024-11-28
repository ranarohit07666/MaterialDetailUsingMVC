using MaterialDetailUsingMVC.BLL.IRepository;
using MaterialDetailUsingMVC.BLL.Repository;
using MaterialDetailUsingMVC.BLL.Service;
using MaterialDetailUsingMVC.BLL.UnitOfWork;
using MaterialDetailUsingMVC.DLL;
using MaterialDetailUsingMVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MaterialDetailUsingMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MaterialDataContext>(o => o.UseSqlServer(config.GetConnectionString("Default")));

            builder.Services.AddDefaultIdentity<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<MaterialDataContext>();

            builder.Services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IMaterialRepository,MaterialRepository>();
            builder.Services.AddScoped<IReferenceDetailRepository,ReferenceDetailRepository>();
            builder.Services.AddScoped<IUnitRepository,UnitRepository>();
            builder.Services.AddScoped<ITypesRepository, TypesRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IService, Service>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Material}/{action=Index}/{id?}"
            );
            app.MapRazorPages();
            CreateRolesAsync(app.Services).GetAwaiter().GetResult();
            app.Run();
        }

        private static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope(); 
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new List<string> { "Admin", "User", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

    }
}
