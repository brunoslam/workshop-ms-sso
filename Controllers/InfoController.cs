using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_OpenIDConnect_DotNet.Models;
using WebApp_OpenIDConnect_DotNet.Services;
using WebApp_OpenIDConnect_DotNet.Utils;

namespace WebApp_OpenIDConnect_DotNet.Controllers
{

    public class InfoController : Controller
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IMSGraphService _msGraphService;

        public InfoController(ITokenAcquisition tokenAcquisition, IMSGraphService msGraphService)
        {
            //_todoItemService = todoItemService;
            _tokenAcquisition = tokenAcquisition;
            _msGraphService = msGraphService;
        }

        // GET: Info
        public ActionResult Index()
        { 
            TempData["UserCalendar"] = !string.IsNullOrEmpty(TempData["UserCalendar"]?.ToString()) ? TempData["UserCalendar"] : JsonConvert.SerializeObject(new List<Eventos>());
            return View();
        }

        // GET: Info/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // Post: Info/BuscarUsuario
        public async Task<ActionResult> BuscarUsuario(InfoUser infoUsuario)
        {
            try
            {
                var userTenant = User.GetTenantId();
                var graphAccessToken = "";
                if (infoUsuario.IsDelegated)
                {
                    graphAccessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { GraphScope.UserReadAll, GraphScope.CalendarsRead }, userTenant);
                }
                var calendarUser = await _msGraphService.GetUsersMeetingsAsync(graphAccessToken, userTenant, infoUsuario);

                if (calendarUser == null)
                {
                    TempData["ErrorMessage"] = "No se encontraron usuarios";
                }
                else
                {
                    TempData["UserCalendar"] = JsonConvert.SerializeObject(calendarUser.ToList());
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }


    }
}
