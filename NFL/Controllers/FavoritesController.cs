﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFL.Models;

namespace NFL.Controllers
{
    public class FavoritesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamListViewModel
            {
                ActiveConf = session.GetActiveConf(),
                ActiveDiv = session.GetActiveDiv(),
                Teams = session.GetMyTeams()
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Delete()
        {
            var session = new NFLSession(HttpContext.Session);
            var cookies = new NFLCookies(Response.Cookies);

            session.RemoveMyTeams();
            cookies.RemoveMyTeamsIDs();

            TempData["message"] = "Favorite Teams are cleared";
            return RedirectToAction("Index","Home", 
                new { activeConf = session.GetActiveConf(), activeDiv = session.GetActiveDiv() });

        }
    }
}
