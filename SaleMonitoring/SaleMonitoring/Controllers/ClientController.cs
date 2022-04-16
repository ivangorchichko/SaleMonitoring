using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Serilog;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.Models.Client;
using SaleMonitoring.PageHelper.Contacts;
using SaleMonitoring.Service.Contracts;

namespace SaleMonitoring.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IPageService _pageService;
        private readonly IClientMapper _clientMapper;
        private readonly ILogger _logger;
        public ClientController(IClientService clientService,  IPageService pageService, IClientMapper clientMapper, ILogger logger)
        {
            _clientService = clientService;
            _pageService = pageService;
            _clientMapper = clientMapper;
            _logger = logger;
        }


        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            _logger.Debug("Running Index Get method in ClientController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            ViewBag.CurrentPage = page;
            _logger.Debug("Sharing Index view");
            return View(_pageService.GetModelsInPageViewModel<ClientViewModel>(page));
        }


        public ActionResult GetClients(int page = 1)
        {
            _logger.Debug("Running GetClients Get method in ClientController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            ViewBag.CurrentPage = page;
            _logger.Debug("Sharing partial view");
            return PartialView("_Clients", _pageService.GetModelsInPageViewModel<ClientViewModel>(page));
        }

        [HttpPost]
        public ActionResult GetClients(string fieldString, TextFieldFilter filter, int page = 1)
        {
            _logger.Debug("Running GetClients Post method in ClientController");
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (filter == TextFieldFilter.Telephone)
                {
                    Regex regex = new Regex(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$");
                    if (regex.IsMatch(fieldString) && (fieldString.Length == 13 || fieldString.Length == 11))
                    {
                        _logger.Debug("Sharing partial view");
                        return PartialView("_Clients", _pageService.GetFilteredModelsInPageViewModel<ClientViewModel>(filter, fieldString, page));
                    }
                    else
                    {
                        _logger.Warning("Incorrect input");
                         ViewBag.NotValidParse = "Неверный ввод номера телефона, примерный ввод : (+375|80)(29|25|44|33)(1111111)";
                         return PartialView("_Clients", _pageService.GetModelsInPageViewModel<ClientViewModel>(page));
                    }
                }
                else
                {
                    _logger.Debug("Sharing partial view");
                    return PartialView("_Clients", _pageService.GetFilteredModelsInPageViewModel<ClientViewModel>(filter, fieldString, page));
                }
            }
            _logger.Debug("Sharing Error view");
            return View("Error");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            _logger.Debug("Running Details Get method in ClientController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Details view");
                return View(_clientMapper.GetClientViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            _logger.Debug("Running Create Get method in ClientController, sharing Create view");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(ClientViewModel clientViewModel)
        {
            _logger.Debug("Running Create Post method in ClientController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Adding new client");
                clientViewModel.Id = _clientService.GetClientsDto().ToList().Count;
                _clientService.AddClient(_clientMapper.GetClientDto(clientViewModel));
                _logger.Debug("Adding complete");
                return View("Details", clientViewModel);
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
            _logger.Debug("Running Modify Get method in ClientController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Modify view");
                return View(_clientMapper.GetClientViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ClientViewModel clientViewModel)
        {
            _logger.Debug("Running Modify Post method in ClientController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Modify client model");
                _clientService.ModifyClient(_clientMapper.GetClientDto(clientViewModel));
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
            _logger.Debug("Running Delete Get method in ClientController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Delete view");
                return View(_clientMapper.GetClientViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(ClientViewModel clientViewModel)
        {
            _logger.Debug("Running Delete Post method in ClientController");
            if (clientViewModel.Id != 0)
            {
                _logger.Debug("Remove client from db");
                _clientService.RemoveClient(_clientMapper.GetClientDto(clientViewModel));
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