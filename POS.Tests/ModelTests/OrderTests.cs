using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using PointOfSale.Models;

namespace PointOfSale.Tests
{
    [TestClass]
    public class OrderTest
    {
        private List<MenuItem> items = new List<MenuItem> { };
        public OrderTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root; password=root;port=8889;database=pos_test;";
        }
        [TestMethod]
        public void Order_CanBeCreated_True()
        {
            Order testOrder = new Order();
            Assert.IsInstanceOfType(testOrder, typeof(Order));
        }

        [TestMethod]
        public void Find_ReturnsCorrectOrder_True()
        {
            Order newOrder = new Order();
            newOrder.Save();
            Order testOrder = Order.Find(newOrder.GetId());
            Console.WriteLine(newOrder.GetId() + " " + testOrder.GetId());
            Assert.AreEqual(newOrder, testOrder);
        }

        [TestMethod]
        public void GetMenuItems_ReturnsCorrectList_MenuItemList()
        {
            string name = "Chicken";
            float price = 13.99f;
            List<string> ingredients = new List<string> { "Chicken", "Salt", "Pepper" };
            MenuItem newItem = new MenuItem(name, price, ingredients);
            newItem.Save();
            Order newOrder = new Order();
            newOrder.Save();
            items.Add(newItem);
            newOrder.AddItem(newItem.GetId());
            Order testOrder = Order.Find(newOrder.GetId());
            List<MenuItem> testList = testOrder.GetMenuItems();
            Console.WriteLine(items.Count + " " + testList.Count);
            CollectionAssert.AreEqual(items, testList);
        }
    }
}