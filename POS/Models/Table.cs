using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PointOfSale.Models
{
    public class Table
    {
        private int Id;
        public Table(int id = 0)
        {
            Id = id;
        }
        public int GetId()
        {
            return Id;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO tbls () VALUES ();";
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
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                Table newTable = new Table(id);
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
            cmd.CommandText = @"SELECT * FROM tables WHERE id=@id;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            return new Table(id); //NEEDS TO CHANGE
        }

        public override bool Equals(System.Object otherTable)
        {
            if (!(otherTable is Table))
            {
                return false;
            }
            Table newTable = otherTable as Table;
            bool idEquality = this.GetId().Equals(newTable.GetId());
            return idEquality;
        }
    }
}