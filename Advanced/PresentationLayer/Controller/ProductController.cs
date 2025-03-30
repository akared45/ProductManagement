using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.ApplicationLayer.DTOs;
using Advanced.ApplicationLayer.Interfaces;

namespace Advanced.PresentationLayer.Controller
{
    public class ProductController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public void DisplayProducts()
        {
            var products = _productService.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Categories: {string.Join(", ", product.Categories)}");
            }
        }
        public void AddProduct(string name, decimal price, List<string> categories)
        {
            var productDto = new ProductDTO { Name = name, Price = price, Categories = categories };
            _productService.AddProduct(productDto);
            Console.WriteLine($"Added product: {name}");
        }
        public void UpdateProduct(int id, string name, decimal price, List<string> categories)
        {
            var productDto = new ProductDTO { Id = id, Name = name, Price = price, Categories = categories };
            _productService.UpdateProduct(productDto);
            Console.WriteLine($"Updated product with ID: {id}");
        }

        public void DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            Console.WriteLine($"Deleted product with ID: {id}");
        }
        public void SearchProducts(string name)
        {
            var results = _productService.SearchProductsByName(name);
            foreach (var result in results)
            {
                Console.WriteLine($"Found - ID: {result.Id}, Name: {result.Name}");
            }
        }
    }
}
