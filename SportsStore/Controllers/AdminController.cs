using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class AdminController: Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => View(_repository.Products);

        [HttpGet]
        public ViewResult Edit(int productId) => View(_repository.Products.FirstOrDefault(x => x.ProductId == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction(nameof(Index));
            }
            else
                return View(product);
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deleteProduct = _repository.DeleteProduct(productId);

            if (deleteProduct != null)
                TempData["message"] = $"{deleteProduct.Name} was deleted";

            return RedirectToAction(nameof(Index));
        }
    }
}
