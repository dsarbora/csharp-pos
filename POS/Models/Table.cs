using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PointOfSale.Models
{
    public class Table
    {
        int CurrentOrderId;
        private int Id;
        public Table(int currentOrderId = -1, int id = 0)
        {
            CurrentOrderId = currentOrderId;
            Id = id;
        }
        public int GetId()
        {
            return Id;
        }

        public int GetCurrentOrderId()
        {
            return CurrentOrderId;
        }

        public void SetCurrentOrderId(int orderId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE tbls SET current_order_id=@orderId WHERE id=@id;";
            cmd.Parameters.Add(new MySqlParameter("@orderId", orderId));
            cmd.Parameters.Add(new MySqlParameter("@id", Id));
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
            cmd.CommandText = @"INSERT INTO tbls (current_order_id) VALUES (@currentOrderId);";
            cmd.Parameters.Add(new MySqlParameter("@currentOrderId", CurrentOrderId));
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
            cmd.CommandText = @"DELETE FROM tbls;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Table> GetAll()
        {
            List<Table> allTables = new List<Table> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM tbls;";
            MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            int currentOrderId = 0;
            while (rdr.Read())
            {
                currentOrderId = rdr.GetInt32(1);
                id = rdr.GetInt32(0);
                Table newTable = new Table(currentOrderId, id);
                allTables.Add(newTable);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTables;
        }

        public static Table Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM tbls WHERE id=@id;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            int currentOrderId = 0;
            while (rdr.Read())
            {
                currentOrderId = rdr.GetInt32(1);
            }
            Table newTable = new Table(currentOrderId, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newTable;
        }

        public override bool Equals(System.Object otherTable)
        {
            if (!(otherTable is Table))
            {
                return false;
            }
            Table newTable = otherTable as Table;
            bool idEquality = this.GetId().Equals(newTable.GetId());
            bool currentOrderIdEquality = this.GetCurrentOrderId().Equals(newTable.GetCurrentOrderId());
            return idEquality && currentOrderIdEquality;
        }
    }
}