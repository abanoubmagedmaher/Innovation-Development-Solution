using Innovation.Development.BLL.Services.Students;
using Innovation.Development.BLL.Services.Subjects;
using Innovation.Development.DAL.contracts;
using Innovation.Development.DAL.contracts.Repositories;
using Innovation.Development.DAL.Persistence.Data;
using Innovation.Development.DAL.Persistence.Repositories;
using Innovation.Development.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Innovation_Development
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"))
            );

            // Register repositories
            builder.Services.AddScoped<IStudentsRepository, StudentRepository>();
            builder.Services.AddScoped<ISubjectsRepository, SubjectRepository>();
            
            // Register unit of work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // Register services
            builder.Services.AddScoped<IStudentService, StudetService>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
