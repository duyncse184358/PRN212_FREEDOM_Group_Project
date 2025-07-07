using System.Configuration;
using System.Data;
using System.Windows;
using DataAccessLayer.DAO;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reponsitories.Implementations;
using Reponsitories;
using Services.Implementations;
using Services;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace LibraryWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureServices();

            using (var scope = Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
                // dbContext.Database.Migrate(); // Comment hoặc bỏ nếu dùng Database First và SQL script
            }

            // Khởi động LoginWindow qua DI
            var loginWindow = Services.GetRequiredService<Views.LoginWindow>();
            loginWindow.Show();
        }

        private void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // ĐÃ SỬA LỖI: Sử dụng Directory.GetCurrentDirectory() thay vì AppContext.BaseDirectory
            string basePath = Directory.GetCurrentDirectory();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show(
                    $"Không tìm thấy ConnectionString 'DefaultConnection' trong appsettings.json tại: {Path.Combine(basePath, "appsettings.json")}",
                    "Lỗi cấu hình",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                Environment.Exit(1);
            }

            // Đăng ký DbContext
            serviceCollection.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            // Đăng ký các DAO
          
            serviceCollection.AddTransient<BookDAO>();
            serviceCollection.AddTransient<BorrowingDAO>();
            serviceCollection.AddTransient<CategoryDAO>();
            serviceCollection.AddTransient<FineDAO>();
            serviceCollection.AddTransient<PatronDAO>();
            serviceCollection.AddTransient<UserDAO>();

            // Đăng ký Repositories
            
            serviceCollection.AddTransient<IBookRepository, BookRepository>();
            serviceCollection.AddTransient<IBorrowingRepository, BorrowingRepository>();
            serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddTransient<IFineRepository, FineRepository>();
            serviceCollection.AddTransient<IPatronRepository, PatronRepository>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();

            // Đăng ký Services 
           
            serviceCollection.AddTransient<IBookService, BookService>();
            serviceCollection.AddTransient<IBorrowingService, BorrowingService>();
            serviceCollection.AddTransient<ICategoryService, CategoryService>();
            serviceCollection.AddTransient<IFineService, FineService>();
            serviceCollection.AddTransient<IPatronService, PatronService>();
            serviceCollection.AddTransient<IUserService, UserService>();

            // Đăng ký ViewModels
            serviceCollection.AddTransient<ViewModels.LoginViewModel>();
            serviceCollection.AddTransient<ViewModels.MainViewModel>();
            serviceCollection.AddTransient<ViewModels.BookViewModel>();
            serviceCollection.AddTransient<ViewModels.BookDialogViewModel>();
            serviceCollection.AddTransient<ViewModels.PatronViewModel>();
            serviceCollection.AddTransient<ViewModels.PatronDialogViewModel>();
            serviceCollection.AddTransient<ViewModels.BorrowReturnViewModel>();
            serviceCollection.AddTransient<ViewModels.OverdueBooksViewModel>();
            serviceCollection.AddTransient<ViewModels.ReportViewModel>();
            serviceCollection.AddTransient<ViewModels.FineManagementViewModel>();
           // serviceCollection.AddTransient<ViewModels.ProfileViewModel>();
            serviceCollection.AddTransient<ViewModels.BorrowBookDialogViewModel>();
            serviceCollection.AddTransient<ViewModels.ReturnBookDialogViewModel>();
            serviceCollection.AddTransient<ViewModels.PatronBorrowingHistoryViewModel>();
            serviceCollection.AddTransient<ViewModels.FinePaymentDialogViewModel>();
            serviceCollection.AddTransient<ViewModels.UserViewModel>();
            serviceCollection.AddTransient<ViewModels.UserDialogViewModel>();
            serviceCollection.AddTransient<ViewModels.CategoryViewModel>();
            serviceCollection.AddTransient<ViewModels.CategoryDialogViewModel>();


            // Đăng ký Views
            serviceCollection.AddTransient<Views.LoginWindow>();
            serviceCollection.AddTransient<Views.MainWindow>();
            serviceCollection.AddTransient<Views.BookWindow>();
            serviceCollection.AddTransient<Views.BookDialog>();
            serviceCollection.AddTransient<Views.PatronWindow>();
            serviceCollection.AddTransient<Views.PatronDialog>();
            serviceCollection.AddTransient<Views.BorrowReturnWindow>();
            serviceCollection.AddTransient<Views.OverdueBooksWindow>();
            serviceCollection.AddTransient<Views.ReportWindow>();
            serviceCollection.AddTransient<Views.FineManagementWindow>();
          //  serviceCollection.AddTransient<Views.ProfileWindow>();
            serviceCollection.AddTransient<Views.BorrowBookDialog>();
            serviceCollection.AddTransient<Views.ReturnBookDialog>();
            serviceCollection.AddTransient<Views.PatronBorrowingHistoryWindow>();
            serviceCollection.AddTransient<Views.FinePaymentDialog>();
            serviceCollection.AddTransient<Views.UserManagementWindow>();
            serviceCollection.AddTransient<Views.UserDialog>();
            serviceCollection.AddTransient<Views.CategoryWindow>();
            serviceCollection.AddTransient<Views.CategoryDialog>();


            Services = serviceCollection.BuildServiceProvider();
        }
    }

}
