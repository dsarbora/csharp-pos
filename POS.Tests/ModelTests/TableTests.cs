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
            Table newTable = new Table();
            Table newNewTable = new Table();
            newTable.Save();
            newNewTable.Save();
            tableList.Add(newTable);
            tableList.Add(newNewTable);
            List<Table> testList = Table.GetAll();

            CollectionAssert.AreEqual(tableList, testList);

        }
    }
}