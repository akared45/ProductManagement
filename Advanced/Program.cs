using Advanced.ApplicationLayer.Services;
using Advanced.InfrastructureLayer.Data;
using Advanced.InfrastructureLayer.Logging;
using Advanced.InfrastructureLayer.Repositories;
using Advanced.PresentationLayer.Controller;
using Advanced.PresentationLayer.Views;
using Microsoft.EntityFrameworkCore;

namespace Advanced
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost;Database=ProductManagement;User=root;Password=;";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)) 
                .Options;

            var context = new ApplicationDbContext(options);
            var productRepo = new Repository<Advanced.DomainLayer.Entities.Product>(context);
            var categoryRepo = new Repository<Advanced.DomainLayer.Entities.Category>(context);
            var logger = new FileLogger();

            var productService = new ProductService(productRepo, categoryRepo, logger, context);
            var categoryService = new CategoryService(categoryRepo, logger, context);

            var productController = new ProductController(productService);
            var categoryController = new CategoryController(categoryService);

            var menuView = new MenuView(productController, categoryController);
            menuView.ShowMenu();
        }
    }
}
