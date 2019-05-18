using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using System;
using System.Collections.Generic;

namespace PointOfSale.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet("/orders")]
        public ActionResult Index()
        {
            return View();
        }
    }
}