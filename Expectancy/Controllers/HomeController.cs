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
            var engine = HttpContext.Session.Get<GameEngine>(gamekey);

            if (engine == null)
                engine = new GameEngine();

            return View(engine);
        }

        public IActionResult NewGame()
        {
            return MakeChoice(0);
        }

        public IActionResult ContinueGame()
        {
            var id = HttpContext.Session.Get<int>(savekey);
            return MakeChoice(id);
        }

        public IActionResult MakeChoice(int id)
        {
            var engine = HttpContext.Session.Get<GameEngine>(gamekey);
            engine.HasSave = true;
            HttpContext.Session.Set(gamekey, engine);
            
            HttpContext.Session.Set(savekey, id);
            var decision = _dataHelper.Get(id);
            return PartialView("Decision", decision);
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
