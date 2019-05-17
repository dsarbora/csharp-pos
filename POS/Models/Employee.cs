using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PointOfSale.Models
{
    public class Employee
    {
        string Name;
        string Position;
        int Id;
        public Employee(string name, string position, int id = 0)
        {
            Name = name;
            Position = position;
            Id = id;
        }

        public string GetName() { return Name; }
        public int GetId() { return Id; }
        public string GetPosition() { return Position; }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO employees (name, position) VALUES (@name, @position);";
            cmd.Parameters.Add(new MySqlParameter("@name", Name));
            cmd.Parameters.Add(new MySqlParameter("@position", Position));
            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Employee Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM employees WHERE id = @id;";
            MySqlParameter prmId = new MySqlParameter("@id", id);
            cmd.Parameters.Add(prmId);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string name = "";
            string position = "";
            while (rdr.Read())
            {
                name = rdr.GetString(1);
                position = rdr.GetString(2);
            }
            Employee foundEmployee = new Employee(name, position, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundEmployee;
        }

        public Order CreateOrder()
        {
            Order newOrder = new Order();
            newOrder.Save();
            return newOrder;
        }

        public override bool Equals(System.Object otherEmployee)
        {
            if (!(otherEmployee is Employee))
            {
                return false;
            }
            Employee newEmployee = otherEmployee as Employee;
            bool nameEquality = this.GetName().Equals(newEmployee.GetName());
            bool idEquality = this.GetId().Equals(newEmployee.GetId());
            bool positionEquality = this.GetPosition().Equals(newEmployee.GetPosition());
            return nameEquality && idEquality && positionEquality;
        }
    }
}