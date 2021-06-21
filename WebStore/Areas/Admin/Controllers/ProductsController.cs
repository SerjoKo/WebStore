﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entitys.Identity;
using WebStore.Servicess.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = Role.Administrators)]
    public class ProductsController : Controller
    {
        private readonly IProductData _ProductData;

        public ProductsController(IProductData ProductData)
        {
            _ProductData = ProductData;
        }
        public IActionResult Index()
        {
            return View(_ProductData.GetProducts());
        }
    }
}
