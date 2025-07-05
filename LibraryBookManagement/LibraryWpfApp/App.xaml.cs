using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using DataAccessLayer;
using Reponsitories;
using Reponsitories.Implementations;
using Services;
using Services.Implementaions;
using Microsoft.EntityFrameworkCore;
using LibraryWpfApp.Windows;

namespace LibraryWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register DbContext
                    services.AddDbContext<LibraryDbContext>(options =>
                        options.UseSqlServer("server=LAPTOP-CMSU7LK1; database=LibraryDB;uid=sa;pwd=12345; TrustServerCertificate=True"));

                    // Register Repositories
                    services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
                    services.AddScoped<IBookRepository, BookRepository>();
                    services.AddScoped<IBorrowingRepository, BorrowingRepository>();
                    services.AddScoped<ICategoryRepository, CategoryRepository>();
                    services.AddScoped<IFineRepository, FineRepository>();
                    services.AddScoped<IPatronRepository, PatronRepository>();
                    services.AddScoped<IReturnRepository, ReturnRepository>();
                    services.AddScoped<IRoleRepository, RoleRepository>();
                    services.AddScoped<IUserRepository, UserRepository>();

                    // Register Services
                    services.AddScoped<IActivityLogService, ActivityLogService>();
                    services.AddScoped<IBookService, BookService>();
                    services.AddScoped<IBorrowingService, BorrowingService>();
                    services.AddScoped<ICategoryService, CategoryService>();
                    services.AddScoped<IFineService, FineService>();
                    services.AddScoped<IPatronService, PatronService>();
                    services.AddScoped<IReturnService, ReturnService>();
                    services.AddScoped<IRoleService, RoleService>();
                    services.AddScoped<IUserService, UserService>();

                    // Register Windows
                    services.AddTransient<MainWindow>();
                    services.AddTransient<LoginWindow>();
                })
                .Build();

            var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose();
            base.OnExit(e);
        }
    }

}
