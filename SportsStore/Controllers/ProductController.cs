using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _reposotiry;
        public int PageSize = 4;

        public ProductController(IProductRepository repository)
        {
            _reposotiry = repository;
        }

        public ViewResult List(string category, int page = 1) => View(new ProductsListViewModel
        {
            Products = _reposotiry.Products
            .Where(x => x.Category == category || category == null)
            .OrderBy(x => x.ProductId)
            .Skip((page - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _reposotiry.Products.Count()
            },
            CurrentCategory = category
        });
    }
}
