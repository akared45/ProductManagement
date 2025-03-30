using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.ApplicationLayer.DTOs;
using Advanced.ApplicationLayer.Interfaces;
using Advanced.DomainLayer.Entities;
using Advanced.DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Advanced.ApplicationLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly Advanced.InfrastructureLayer.Logging.FileLogger _fileLogger;
        private readonly Advanced.InfrastructureLayer.Data.ApplicationDbContext _applicationDbContext;
        public ProductService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository,
            Advanced.InfrastructureLayer.Logging.FileLogger fileLogger,
            Advanced.InfrastructureLayer.Data.ApplicationDbContext applicationDbContext)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _fileLogger = fileLogger;
            _applicationDbContext = applicationDbContext;
        }
        public void UpdateProduct(ProductDTO productDTO)
        {
            var product = _productRepository.GetById(productDTO.Id);
            if (product != null)
            {
                product.Name = productDTO.Name;
                product.Price = productDTO.Price;

                var existingCategories = product.ProductCategories.ToList();
                foreach (var pc in existingCategories)
                {
                    product.ProductCategories.Remove(pc);
                }
                _applicationDbContext.SaveChanges();

                foreach (var categoryName in productDTO.Categories)
                {
                    var category = _categoryRepository.GetAll().FirstOrDefault(c => c.Name == categoryName);
                    if (category == null)
                    {
                        category = new Category { Name = categoryName, ProductCategories = new List<ProductCategory>() };
                        _categoryRepository.Add(category);
                        _applicationDbContext.SaveChanges();
                    }
                    product.ProductCategories.Add(new ProductCategory { CategoryId = category.Id });
                }

                _productRepository.Update(product);
                _applicationDbContext.SaveChanges();
            }
        }
        public void AddProduct(ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                ProductCategories = new List<ProductCategory>()
            };
            foreach (var categoryName in productDTO.Categories)
            {
                var category = _categoryRepository.GetAll().FirstOrDefault(c => c.Name == categoryName);
                if (category == null)
                {
                    category = new Category
                    {
                        Name = categoryName,
                        ProductCategories = new List<ProductCategory>()
                    };
                    _categoryRepository.Add(category);
                    _applicationDbContext.SaveChanges();
                }
                product.ProductCategories.Add(new ProductCategory { CategoryId = category.Id });
            }
            _productRepository.Add(product);
            _applicationDbContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(product);
                _applicationDbContext.SaveChanges();
                _fileLogger.Log($"Product {product.Name} (ID: {id}) was deleted at {DateTime.Now}");
            }
        }
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = _productRepository.GetAll();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Categories = p.ProductCategories.Select(pc => pc.Category.Name).ToList()
            });
        }
        public ProductDTO GetProductById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return null;
            }
            else
            {
                return new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Categories = product.ProductCategories.Select(pc => pc.Category.Name).ToList()
                };
            }
        }

        public IEnumerable<ProductDTO> SearchProductsByName(string name)
        {
            var products = _productRepository.GetAll();
            return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                          .Select(p => new ProductDTO
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Price = p.Price,
                              Categories = p.ProductCategories.Select(pc => pc.Category.Name).ToList()
                          });
        }
    }
}
