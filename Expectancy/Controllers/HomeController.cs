using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Expectancy.Helpers;
using Expectancy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Expectancy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataHelper _dataHelper;
        private readonly string savekey = "currentDecision";

        public HomeController(ILogger<HomeController> logger, DataHelper dataHelper)
        {
            _logger = logger;
            _dataHelper = dataHelper;
        }

        public IActionResult Index()
        {
            var decisionId = HttpContext.Session.Get<int>(savekey);
            var decision = _dataHelper.Get(decisionId);

            return View(decision);
        }

        public IActionResult NewGame()
        {
            var decision = _dataHelper.Get(0);
            HttpContext.Session.Set(savekey, 0);

            return View("Index", decision);
        }

        public IActionResult ContinueGame()
        {
            var decisionId = HttpContext.Session.Get<int>(savekey);
            var decision = _dataHelper.Get(decisionId);

            return View("Index", decision);
        }

        [HttpGet]
        public IActionResult Decision(int id)
        {
            HttpContext.Session.Set(savekey, id);
            var decision = _dataHelper.Get(id);

            return PartialView(decision);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
