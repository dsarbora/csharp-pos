using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using System.Collections.Generic;
using System;

namespace PointOfSale.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet("/employees")]
        public ActionResult Index()
        {
            List<Table> allTables = Table.GetAll();
            return View(allTables);
        }
    }
}