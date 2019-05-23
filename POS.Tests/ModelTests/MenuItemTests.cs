using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using PointOfSale.Models;

namespace PointOfSale.Tests
{
    [TestClass]
    public class MenuItemTests : IDisposable
    {
        public void Dispose()
        {
            MenuItem.ClearAll();
        }
        [TestMethod]
        public void MenuItem_ReturnsCorrectName_String()
        {
            string name = "Roasted Chicken";
            float price = 13.99f;
            List<string> ingredients = new List<string> { "Chicken", "Salt" };
            MenuItem testItem = new MenuItem(name, price, ingredients);
            Assert.AreEqual(name, testItem.GetName());
        }
        [TestMethod]
        public void MenuItem_ReturnsCorrectIngredients_StringList()
        {
            string name = "Roasted Chicken";
            float price = 13.99f;
            List<string> ingredients = new List<string> { "Chicken", "Salt" };
            MenuItem testItem = new MenuItem(name, price, ingredients);
            CollectionAssert.AreEqual(ingredients, testItem.GetIngredients());
        }

        [TestMethod]
        public void Price_ReturnsCorrectPrice_Float()
        {
            string name = "Roasted Chicken";
            float price = 13.99f;
            List<string> ingredients = new List<string> { "Chicken", "Salt" };
            MenuItem testItem = new MenuItem(name, price, ingredients);
            Assert.AreEqual(price, testItem.GetPrice());
        }

        [TestMethod]
        public void Find_ReturnsCorrectMenuItem_MenuItem()
        {
            MenuItem item = new MenuItem("Chicken", 13.99f, new List<string> { "chicken" });
            item.Save();
            MenuItem testItem = MenuItem.Find(item.GetId());
            Assert.AreEqual(item.GetName(), testItem.GetName());

        }

        [TestMethod]
        public void OverrideEquals_ActuallyOverridesEquals_True()
        {
            MenuItem item = new MenuItem("Chicken", 13.99f, new List<string> { "chicken" });
            item.Save();
            MenuItem testItem = MenuItem.Find(item.GetId());
            Assert.AreEqual(item, testItem);
        }

        [TestMethod]
        public void GetAll_ReturnsAllMenuItems_MenuItemList()
        {
            MenuItem item = new MenuItem("Chicken", 13.99f, new List<string> { "chicken" });
            item.Save();
            MenuItem item2 = new MenuItem("Mushrooms", 5f, new List<string> { "mushrooms" });
            item2.Save();
            List<MenuItem> allItems = new List<MenuItem> { item, item2 };
            List<MenuItem> testList = MenuItem.GetAll();
            CollectionAssert.AreEqual(allItems, testList);
        }

        [TestMethod]

        public void Delete_DeletesMenuItem_ItemList()
        {
            MenuItem item = new MenuItem("Chicken", 13.99f, new List<string> { "chicken" });
            item.Save();
            MenuItem item2 = new MenuItem("Mushrooms", 5f, new List<string> { "mushrooms" });
            item2.Save();
            MenuItem.Delete(item2.GetId());
            List<MenuItem> allItems = new List<MenuItem> { item };
            List<MenuItem> testList = MenuItem.GetAll();
            CollectionAssert.AreEqual(allItems, testList);
        }

    }
}