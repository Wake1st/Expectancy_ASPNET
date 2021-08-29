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
            var vm = HttpContext.Session.Get<HomeViewModel>(gamekey);

            if (vm == null)
                vm = new HomeViewModel();

            return View(vm);
        }

        public IActionResult NewGame()
        {
            var vm = new HomeViewModel();
            vm.HasGame = true;
            HttpContext.Session.Set(gamekey, vm);

            return View("Index",vm);
        }

        public IActionResult ContinueGame()
        {
            var vm = new HomeViewModel();
            vm.HasGame = true;
            vm.GameEngine = HttpContext.Session.Get<GameEngine>(gamekey);

            return View("Index",vm);
        }

        public IActionResult MakeChoice(int id)
        {
            var engine = HttpContext.Session.Get<GameEngine>(savekey);
            engine.CurrentDecision = _dataHelper.Get(id);
            HttpContext.Session.Set(savekey, engine);
            
            return PartialView("Decision", engine.CurrentDecision);
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
