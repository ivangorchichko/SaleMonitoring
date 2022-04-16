using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Serilog;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.Models.Client;
using SaleMonitoring.Models.Manager;
using SaleMonitoring.PageHelper.Contacts;
using SaleMonitoring.Service.Contracts;

namespace SaleMonitoring.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IPageService _pageService;
        private readonly IManagerMapper _managerMapper;
        private readonly ILogger _logger;

        public ManagerController(IManagerService managerService, ILogger logger, IPageService pageService, IManagerMapper managerMapper)
        {
            _managerService = managerService;
            _logger = logger;
            _pageService = pageService;
            _managerMapper = managerMapper;
        }


        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            _logger.Debug("Running Index Get method in ManagerController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            ViewBag.CurrentPage = page;
            _logger.Debug("Sharing Index view");
            return View(_pageService.GetModelsInPageViewModel<ManagerViewModel>(page));
        }

        public ActionResult GetManagers(int page = 1)
        {
            _logger.Debug("Running GetManagers Get method in ManagerController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            ViewBag.CurrentPage = page;
            _logger.Debug("Sharing partial view");
            return PartialView("_Managers",_pageService.GetModelsInPageViewModel<ManagerViewModel>(page));
        }


        [HttpPost]
        public ActionResult GetManagers(string fieldString, TextFieldFilter filter, int page = 1)
        {
            _logger.Debug("Running GetManagers Post method in ManagerController");
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (filter == TextFieldFilter.Telephone)
                {
                    Regex regex = new Regex(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$");
                    if (regex.IsMatch(fieldString) && (fieldString.Length == 13 || fieldString.Length == 11))
                    {
                        _logger.Debug("Sharing partial view");
                        return PartialView("_Managers", _pageService.GetFilteredModelsInPageViewModel<ManagerViewModel>(filter, fieldString, page));
                    }
                    else
                    {
                        _logger.Warning("Incorrect input");
                        ViewBag.NotValidParse = "Неверный ввод номера телефона, примерный ввод : (+375|80)(29|25|44|33)(1111111)";
                        return PartialView("_Managers", _pageService.GetModelsInPageViewModel<ManagerViewModel>(page));
                    }
                }
                else
                {
                    _logger.Debug("Sharing partial view");
                    return PartialView("_Managers", _pageService.GetFilteredModelsInPageViewModel<ManagerViewModel>(filter, fieldString, page));
                }
            }
            _logger.Debug("Sharing Error view");
            return View("Error");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            _logger.Debug("Running Details Get method in ManagerController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Details view");
                return View(_managerMapper.GetManagerViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            _logger.Debug("Running Create Get method in ManagerController, sharing Create view");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(ManagerViewModel managerViewModel)
        {
            _logger.Debug("Running Create Post method in ManagerController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Adding new manager");
                managerViewModel.Id = _managerService.GetManagersDto().ToList().Count;
                _managerService.AddManager(_managerMapper.GetManagerDto(managerViewModel));
                _logger.Debug("Adding complete");
                return View("Details", managerViewModel);
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
            _logger.Debug("Running Modify Get method in ManagerController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Modify view");
                return View(_managerMapper.GetManagerViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ManagerViewModel managerViewModel)
        {
            _logger.Debug("Running Modify Post method in ManagerController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Modify manager model");
                _managerService.ModifyManager(_managerMapper.GetManagerDto(managerViewModel));
                _logger.Debug("Modify complete");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.Error("Error in model state");
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            _logger.Debug("Running Delete Get method in ManagerController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Delete view");
                return View(_managerMapper.GetManagerViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(ManagerViewModel managerViewModel)
        {
            _logger.Debug("Running Delete Post method in ManagerController");
            if (managerViewModel.Id != 0)
            {
                _logger.Debug("Remove manager from db");
                _managerService.RemoveManager(_managerMapper.GetManagerDto(managerViewModel));
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