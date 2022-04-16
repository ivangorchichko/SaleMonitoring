using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Serilog;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.Models.Purchase;
using SaleMonitoring.PageHelper.Contacts;
using SaleMonitoring.Service.Contracts;

namespace SaleMonitoring.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IPageService _pageService;
        private readonly IPurchaseMapper _purchaseMapper;
        private readonly ILogger _logger;

        public PurchaseController(IPurchaseService purchaseService, IPageService pageService, IPurchaseMapper purchaseMapper, ILogger logger)
        {
            _purchaseService = purchaseService;
            _pageService = pageService;
            _purchaseMapper = purchaseMapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            _logger.Debug("Running Index Get method in PurchaseController");
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            _logger.Debug("Sharing Index view");
            ViewBag.CurrentPage = page;
            return View();
        }

        public ActionResult GetPurchases(int page = 1)
        {
            _logger.Debug("Running GetPurchases Get method in PurchaseController");
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            _logger.Debug("Sharing partial view");
            ViewBag.CurrentPage = page;
            return PartialView("_Purchases",_pageService.GetModelsInPageViewModel<PurchaseViewModel>(page));
        }

        [HttpPost]
        public ActionResult GetPurchases(string fieldString, TextFieldFilter filter, int page = 1)
        {
            _logger.Debug("Running GetPurchases Post method in PurchaseController");
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (filter == TextFieldFilter.Date)
                {
                    try
                    {
                        DateTime.Parse(fieldString);
                        _logger.Debug("Sharing partial view");
                        return PartialView("_Purchases",
                            _pageService.GetFilteredModelsInPageViewModel<PurchaseViewModel>(filter, fieldString,
                                page));
                    }
                    catch (Exception e)
                    {
                        _logger.Warning("Incorrect input");
                        ViewBag.NotValidParse = "Неверный ввод даты, примерный ввод : 11.11.2011";
                        return PartialView("_Purchases",_pageService.GetModelsInPageViewModel<PurchaseViewModel>(page));
                    }
                }
                _logger.Debug("Sharing partial view");
                return PartialView("_Purchases",
                    _pageService.GetFilteredModelsInPageViewModel<PurchaseViewModel>(filter, fieldString,
                        page));
            }
            _logger.Debug("Sharing Error view");
            return View("Error");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            _logger.Debug("Running Details Get method in PurchaseController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Details view");
                return View(_purchaseMapper.GetPurchaseViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            _logger.Debug("Running Create Get method in PurchaseController, sharing Create view");
            return View(_purchaseMapper.GetManagersViewModel());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(CreatePurchaseViewModel purchaseViewModel)
        {
            _logger.Debug("Running Create Post method in PurchaseController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Adding new purchase");
                _purchaseService.AddPurchase(_purchaseMapper.GetPurchaseDto(purchaseViewModel));
                _logger.Debug("Adding complete");
                return View("Details", _purchaseMapper.GetPurchaseViewModel(purchaseViewModel));
            }
            else
            {
                _logger.Error("Error in model state");
                return View(_purchaseMapper.GetManagersViewModel());
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Modify(int? id)
        {
            _logger.Debug("Running Modify Get method in PurchaseController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Modify view");
                return View(_purchaseMapper.GetModifyPurchaseViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ModifyPurchaseViewModel purchaseViewModel)
        {
            _logger.Debug("Running Modify Post method in PurchaseController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Modify purchase model");
                _purchaseService.ModifyPurchase(_purchaseMapper.GetPurchaseDto(purchaseViewModel));
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
            _logger.Debug("Running Delete Get method in PurchaseController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Delete view");
                return View(_purchaseMapper.GetPurchaseViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(PurchaseViewModel purchaseViewModel)
        {
            _logger.Debug("Running Delete Post method in PurchaseController");
            if (purchaseViewModel.Id != 0)
            {
                _logger.Debug("Remove purchase from db");
                _purchaseService.RemovePurchase(_purchaseMapper.GetPurchaseDto(_purchaseMapper.GetPurchaseViewModel(purchaseViewModel.Id)));
                _logger.Debug("Remove complete");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.Error("Error in chosen model");
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetChartData()
        {
            _logger.Debug("Running GetChartData method in purchaseController");
            var item = _purchaseService.GetPurchaseDto()
                .GroupBy(x => x.Date.ToString("d"))
                .Select(x => new object[] { x.Key.ToString(), x.Count() })
                .ToArray();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}
