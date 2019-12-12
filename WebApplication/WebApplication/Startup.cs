using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Models;

namespace WebApplication
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ApplicationDbContext context)
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //Test
            CreateRoles(serviceProvider).Wait();
            CreateTestUsers(serviceProvider, context).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Employee", "SuperAdmin", "Admin", "Lecturer", "Student" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var email = "superadmin@mywebapp.com";

            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser();
                user.Id = "5e21a16c-e583-4fc2-a786-d763d0c4a8d1";
                user.UserName = email;
                user.Email = email;
                user.FirstName = "Administrator";
                user.LastName = "Super";
                user.EmailConfirmed = true;
                var createUserResult = await userManager.CreateAsync(user, "A@dmin123456");
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                    await userManager.AddToRoleAsync(user, "SuperAdmin");
                }
            }
        }

        private async Task CreateTestUsers(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            var email = "hutechlecturer@mywebapp.com";
            string lecturerId = "";
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser();
                user.Id = "50717a31-498c-434a-972b-d79c0b9453a7";
                user.UserName = email;
                user.Email = email;
                user.FirstName = "Khiem";
                user.LastName = "Pham";
                user.EmailConfirmed = true;
                var createUserResult = await userManager.CreateAsync(user, "A@dmin123456");
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                    await userManager.AddToRoleAsync(user, "Lecturer");
                }
                lecturerId = user.Id;
                context.Add(new Lecturer {LecturerId = lecturerId, IsManager = false });
                await context.SaveChangesAsync();

            }

            email = "hutechstudent@mywebapp.com";

            user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser();
                user.Id = "dc291a7b-65b1-4a7f-a7c5-5e8dfef5e122";
                user.UserName = email;
                user.Email = email;
                user.FirstName = "Nhat";
                user.LastName = "Hong";
                user.EmailConfirmed = true;
                var createUserResult = await userManager.CreateAsync(user, "A@dmin123456");
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Student");
                }
                context.Add(new Student { StudentId = user.Id, StudentCode = "1611061161", LecturerId = lecturerId });
                await context.SaveChangesAsync();
            }
        }
    }
}
