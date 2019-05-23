using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using PointOfSale.Models;

namespace PointOfSale.Tests
{
    [TestClass]
    public class TableTests : IDisposable
    {

        public TableTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root; password=root;port=8889;database=pos_test;";
        }

        public void Dispose()
        {
            Table.ClearAll();
        }
        [TestMethod]

        public void GetAll_ReturnsAllTables_TableList()
        {
            List<Table> tableList = new List<Table> { };
            Table newTable = new Table(-1);
            Table newNewTable = new Table(-1);
            newTable.Save();
            newNewTable.Save();
            tableList.Add(newTable);
            tableList.Add(newNewTable);
            List<Table> testList = Table.GetAll();

            CollectionAssert.AreEqual(tableList, testList);

        }

        [TestMethod]
        public void SetOrderId_SetsOrderId_True()
        {
            Table table = new Table();
            Order order = new Order();
            table.Save();
            order.Save();
            table.SetCurrentOrderId(order.GetId());
            Table testTable = Table.Find(table.GetId());
            Assert.AreNotEqual(testTable.GetCurrentOrderId(), -1);
            Assert.AreEqual(testTable.GetCurrentOrderId(), order.GetId());
        }
    }
}