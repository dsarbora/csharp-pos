using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PointOfSale.Models
{
    public class Order
    {
        private int Id;
        private List<MenuItem> Items;

        public Order(int id = 0)
        {
            Id = id;
            Items = new List<MenuItem> { };
        }

        public int GetId()
        { return Id; }

        public List<MenuItem> GetMenuItems()
        {
            List<MenuItem> items = new List<MenuItem> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM menu_items m_i JOIN menu_items_orders m_i_o ON (m_i_o.menu_item_id=m_i.id) WHERE m_i_o.order_id = @id;";
            cmd.Parameters.Add(new MySqlParameter("@id", Id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            string name = "";
            float price = 0f;
            List<string> ingredients = new List<string> { "Chicken", "Lemon" };
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
                price = rdr.GetFloat(2);
                MenuItem item = new MenuItem(name, price, ingredients, id);
                items.Add(item);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return items;
        }

        public void SetItems(List<MenuItem> input)
        {
            Items = input;
        }

        public void AddItem(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO menu_items_orders (menu_item_id, order_id) VALUES (@itemId, @orderId);";
            cmd.Parameters.Add(new MySqlParameter("@itemId", id));
            cmd.Parameters.Add(new MySqlParameter("@orderId", Id));
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO orders () VALUES ();";
            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Order Find(int id)
        {
            Order newOrder = new Order(id);
            newOrder.SetItems(newOrder.GetMenuItems());
            return newOrder;
            // List<MenuItem> items =  
            // MySqlConnection conn = DB.Connection();
            // conn.Open();
            // MySqlCommand cmd = conn.CreateCommand();
            // cmd.CommandText = @"SELECT * FROM orders WHERE id=@id;";
            // cmd.Parameters.Add(new MySqlParameter("@id", id));
            // MySqlDataReader rdr = cmd.ExecuteReader();
            // List<MenuItem> items = this.GetMenuItems();
            // while (rdr.Read())
            // {

            // }
            // Order newOrder = new Order(items, id);
            // return newOrder;
        }






        public override bool Equals(System.Object otherOrder)
        {
            if (!(otherOrder is Order))
            {
                return false;
            }
            else
            {
                Order newOrder = (Order)otherOrder;
                bool idEquality = this.GetId().Equals(newOrder.GetId());
                bool contentsEquality()

                {
                    List<MenuItem> thisList = this.GetMenuItems();
                    List<MenuItem> thatList = newOrder.GetMenuItems();
                    if (thisList.Count != thatList.Count)
                    {
                        return false;
                    }
                    for (int i = 0; i < thisList.Count; i++)
                    {
                        if (thisList[i] != thatList[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return idEquality && contentsEquality();
            }
        }
    }
}