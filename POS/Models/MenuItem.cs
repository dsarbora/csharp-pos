using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PointOfSale.Models
{
    public class MenuItem
    {
        int Id;
        private string Name;
        private float Price;
        private List<string> Ingredients;
        public MenuItem(string name, float price, List<string> ingredients, int id = 0)
        {
            Name = name;
            Price = price;
            Ingredients = ingredients;
            Id = id;
        }

        public string GetName() { return Name; }
        public int GetId() { return Id; }
        public List<string> GetIngredients() { return Ingredients; }
        public float GetPrice() { return Price; }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO menu_items (name, price) VALUES (@name, @price);";//, @description);";
            cmd.Parameters.Add(new MySqlParameter("@name", Name));
            cmd.Parameters.Add(new MySqlParameter("@price", Price));
            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM menu_items;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM menu_items WHERE id=@id;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static MenuItem Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM menu_items WHERE id=@id;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            string name = "";
            float price = 0f;
            while (rdr.Read())
            {
                name = rdr.GetString(1);
                price = rdr.GetFloat(2);
            }
            return new MenuItem(name, price, new List<string> { }, id);
        }

        public static List<MenuItem> GetAll()
        {
            List<MenuItem> allItems = new List<MenuItem> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM menu_items;";
            MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            string name = "";
            float price = 0f;
            List<string> ingredients = new List<string> { };
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
                price = rdr.GetFloat(2);
                MenuItem newMenuItem = new MenuItem(name, price, ingredients, id);
                allItems.Add(newMenuItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allItems;
        }

        public override bool Equals(System.Object otherMenuItem)
        {
            if (!(otherMenuItem is MenuItem))
            {
                return false;
            }
            MenuItem newItem = otherMenuItem as MenuItem;
            bool idEquality = this.GetId().Equals(newItem.GetId());
            bool nameEquality = this.GetName().Equals(newItem.GetName());
            bool priceEquality = this.GetPrice().Equals(newItem.GetPrice());
            return idEquality && nameEquality && priceEquality;
        }
    }

}