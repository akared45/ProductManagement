using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.PresentationLayer.Controller;

namespace Advanced.PresentationLayer.Views
{
    public class MenuView
    {
        private readonly ProductController _productController;
        private readonly CategoryController _categoryController;
        public MenuView(ProductController productController, CategoryController categoryController)
        {
            _productController = productController;
            _categoryController = categoryController;
        }
        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Product Management System ===");
                Console.WriteLine("1. List all products");
                Console.WriteLine("2. Add product");
                Console.WriteLine("3. Update product");
                Console.WriteLine("4. Delete product");
                Console.WriteLine("5. Search products by name");
                Console.WriteLine("6. List all categories");
                Console.WriteLine("7. Add category");
                Console.WriteLine("8. Update category");
                Console.WriteLine("9. Delete category");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _productController.DisplayProducts();
                        break;
                    case "2":
                        Console.Write("Enter product name: ");
                        var name = Console.ReadLine();
                        Console.Write("Enter price: ");
                        var price = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter categories (comma-separated): ");
                        var categories = Console.ReadLine().Split(',').Select(c => c.Trim()).ToList();
                        _productController.AddProduct(name, price, categories);
                        break;
                    case "3":
                        Console.Write("Enter product ID: ");
                        var id = int.Parse(Console.ReadLine());
                        Console.Write("Enter new name: ");
                        var newName = Console.ReadLine();
                        Console.Write("Enter new price: ");
                        var newPrice = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter new categories (comma-separated): ");
                        var newCategories = Console.ReadLine().Split(',').Select(c => c.Trim()).ToList();
                        _productController.UpdateProduct(id, newName, newPrice, newCategories);
                        break;
                    case "4":
                        Console.Write("Enter product ID: ");
                        var delId = int.Parse(Console.ReadLine());
                        _productController.DeleteProduct(delId);
                        break;
                    case "5":
                        Console.Write("Enter search term: ");
                        var searchTerm = Console.ReadLine();
                        _productController.SearchProducts(searchTerm);
                        break;
                    case "6":
                        _categoryController.DisplayAllCategories();
                        break;
                    case "7":
                        Console.Write("Enter category name: ");
                        var catName = Console.ReadLine();
                        _categoryController.AddCategory(catName);
                        break;
                    case "8":
                        Console.Write("Enter category ID: ");
                        var catId = int.Parse(Console.ReadLine());
                        Console.Write("Enter new name: ");
                        var catNewName = Console.ReadLine();
                        _categoryController.UpdateCategory(catId, catNewName);
                        break;
                    case "9":
                        Console.Write("Enter category ID: ");
                        var delCatId = int.Parse(Console.ReadLine());
                        _categoryController.DeleteCategory(delCatId);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
