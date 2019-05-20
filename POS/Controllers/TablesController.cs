using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using System;
using System.Collections.Generic;

namespace PointOfSale.Controllers
{
    public class TablesController : Controller
    {
        [HttpGet("/tables/{tableId}")]
        public ActionResult Show(int tableId)
        {
            Table table = Table.Find(tableId);
            return View(table);
        }
    }
}