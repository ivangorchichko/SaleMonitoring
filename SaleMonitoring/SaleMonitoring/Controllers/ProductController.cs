using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Serilog;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.Models.Product;
using SaleMonitoring.PageHelper.Contacts;
using SaleMonitoring.Service.Contracts;

namespace SaleMonitoring.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPageService _pageService;
        private readonly IProductMapper _productMapper;
        private readonly ILogger _logger;

        public ProductController(IProductService productService, IPageService pageService, IProductMapper productMapper, ILogger logger)
        {
            _productService = productService;
            _pageService = pageService;
            _productMapper = productMapper;
            _logger = logger;
        }


        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            _logger.Debug("Running Index Get method in ProductController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            ViewBag.CurrentPage = page;
            _logger.Debug("Sharing Index view");
            return View();
        }

        public ActionResult GetProducts(int page = 1)
        {
            _logger.Debug("Running GetProducts Get method in ProductController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            _logger.Debug("Sharing partial view");
            ViewBag.CurrentPage = page;
            return PartialView("_Products", _pageService.GetModelsInPageViewModel<ProductViewModel>(page));
        }

        [HttpPost]
        public ActionResult GetProducts(string fieldString, TextFieldFilter filter, int page = 1)
        {
            _logger.Debug("Running GetProducts Post method in ProductController");
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (filter == TextFieldFilter.Price)
                {
                    try
                    {
                        Double.Parse(fieldString);
                        _logger.Debug("Sharing partial view");
                        return PartialView("_Products",
                            _pageService.GetFilteredModelsInPageViewModel<ProductViewModel>(filter, fieldString, page));
                    }
                    catch (Exception e)
                    {
                        _logger.Warning("Incorrect input");
                        ViewBag.NotValidParse = "Неверный ввод цены, примерный ввод : 0,3";
                        return PartialView("_Products", _pageService.GetModelsInPageViewModel<ProductViewModel>(page));
                    }
                }
                else
                {
                    _logger.Debug("Sharing partial view");
                    return PartialView("_Products",
                        _pageService.GetFilteredModelsInPageViewModel<ProductViewModel>(filter, fieldString, page));
                }
            }
            _logger.Debug("Sharing Error view");
            return View("Error");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            _logger.Debug("Running Details Get method in ProductController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Details view");
                return View(_productMapper.GetProductViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            _logger.Debug("Running Create Get method in ProductController, sharing Create view");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            _logger.Debug("Running Create Post method in ProductController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Adding new product");
                productViewModel.Id = _productService.GetProductsDto().ToList().Count;
                _productService.AddProduct(_productMapper.GetProductDto(productViewModel));
                _logger.Debug("Adding complete");
                return View("Details", productViewModel);
            }
            else
            {
                _logger.Error("Error in model state");
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Modify(int? id)
        {
            _logger.Debug("Running Modify Get method in ProductController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Modify view");
                return View(_productMapper.GetProductViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ProductViewModel productViewModel)
        {
            _logger.Debug("Running Modify Post method in ProductController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Modify product model");
                _productService.ModifyProduct(_productMapper.GetProductDto(productViewModel));
                _logger.Debug("Modify complete");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.Error("Error in models state");
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            _logger.Debug("Running Delete Get method in ProductController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Delete view");
                return View(_productMapper.GetProductViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(ProductViewModel productViewModel)
        {
            _logger.Debug("Running Delete Post method in ProductController");
            if (productViewModel.Id != 0)
            {
                _logger.Debug("Remove product from db");
                _productService.RemoveProduct(_productMapper.GetProductDto(productViewModel));
                _logger.Debug("Remove complete");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.Error("Error in chosen model");
                return View();
            }
        }
    }
}