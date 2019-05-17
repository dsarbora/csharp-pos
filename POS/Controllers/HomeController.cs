using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using System.Collections.Generic;
using System;


namespace PointOfSale.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
