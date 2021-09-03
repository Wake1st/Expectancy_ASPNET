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
        private readonly string savekey = "savedata";
        private readonly string gamekey = "gameengine";

        public HomeController(ILogger<HomeController> logger, DataHelper dataHelper)
        {
            _logger = logger;
            _dataHelper = dataHelper;
        }

        public IActionResult Index()
        {
            //var vm = HttpContext.Session.Get<HomeViewModel>(gamekey);

            //if (vm == null)
            //    vm = new HomeViewModel();

            var decision = _dataHelper.Get(0);

            return View(decision);
        }

        public IActionResult NewGame()
        {
            var vm = new HomeViewModel();
            vm.HasGame = true;
            vm.GameEngine = new GameEngine();
            vm.GameEngine.CurrentDecision = _dataHelper.Get(0);

            HttpContext.Session.Set(gamekey, vm);

            return View("Index",vm);
        }

        public IActionResult ContinueGame()
        {
            var vm = new HomeViewModel();
            vm.HasGame = true;
            vm = HttpContext.Session.Get<HomeViewModel>(gamekey);

            return View("Index",vm);
        }

        [HttpGet]
        public IActionResult Decision(int id)
        {
            var decision = _dataHelper.Get(id);

            //var vm = HttpContext.Session.Get<HomeViewModel>(gamekey);
            //vm.GameEngine.CurrentDecision = decision;
            //HttpContext.Session.Set(gamekey, vm);

            return PartialView(decision);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult MakeChoice(int id, [Bind("CurrentDecision")] GameEngine engine)
        //{
        //    var decision = _dataHelper.Get(id);
        //    engine.CurrentDecision = decision;

        //    var vm = HttpContext.Session.Get<HomeViewModel>(gamekey);
        //    vm.GameEngine = engine;

        //    HttpContext.Session.Set(gamekey, vm);

        //    return PartialView("Decision", decision);
        //}

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
