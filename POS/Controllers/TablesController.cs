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
            Dictionary<string, object> model = new Dictionary<string, object> { };
            Table table = Table.Find(tableId);
            Order order = new Order();
            if (table.GetCurrentOrderId() != -1)
            {
                order = Order.Find(table.GetCurrentOrderId());
            }
            model.Add("table", table);
            model.Add("order", order);
            return View(model);
        }
    }
}