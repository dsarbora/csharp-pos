using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using PointOfSale.Models;

namespace PointOfSale.Tests
{
    [TestClass]
    public class EmployeeTest : IDisposable
    {
        string employeeName = "Jacob";
        string position = "server";
        public EmployeeTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root; password=root;port=8889;database=pos_test;";
        }

        public void Dispose()
        {
            Employee.ClearAll();
        }
        [TestMethod]
        public void Employee_HasCorrectName_String()
        {
            Employee testEmployee = new Employee(employeeName, position);
            string testName = testEmployee.GetName();
            Assert.AreEqual(employeeName, testName);
        }
        [TestMethod]
        public void Employee_HasCorrectPosition()
        {
            Employee testEmployee = new Employee(employeeName, position);
            string testPosition = testEmployee.GetPosition();
            Assert.AreEqual(position, testPosition);
        }
        [TestMethod]
        public void Find_ReturnsCorrectEmployee_Employee()
        {
            Employee newEmployee = new Employee(employeeName, position);
            newEmployee.Save();
            Employee testEmployee = Employee.Find(newEmployee.GetId());
            Assert.AreEqual(newEmployee, testEmployee);


        }

        [TestMethod]
        public void Employee_CanCreateOrder_Order()
        {
            Employee newEmployee = new Employee(employeeName, position);
            Order order = newEmployee.CreateOrder();
            Assert.IsInstanceOfType(order, typeof(Order));
        }

        [TestMethod]
        public void GetAll_ReturnsAllEmployees_EmployeeList()
        {
            Employee newEmployee = new Employee(employeeName, position);
            Employee newEmployee2 = new Employee("John", "testPosition");
            newEmployee.Save();
            newEmployee2.Save();
            List<Employee> allEmployees = new List<Employee> { newEmployee, newEmployee2 };
            List<Employee> testList = Employee.GetAll();
            CollectionAssert.AreEqual(allEmployees, testList);
        }

        [TestMethod]
        public void Delete_DeletesCorrectEmployee_EmployeeList()
        {
            Employee newEmployee = new Employee(employeeName, position);
            newEmployee.Save();
            Employee.Delete(newEmployee.GetId());
            List<Employee> allEmployees = new List<Employee> { };
            List<Employee> testList = Employee.GetAll();
            CollectionAssert.AreEqual(allEmployees, testList);
        }
    }
}