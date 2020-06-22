using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;

using AspMVCex.Models;
using MySql.Data.MySqlClient;

namespace AspMVCex.DataAbstractionLayer
{
    public class DB
    {
        private string connectionString = "server=localhost;uid=root;pwd=;database=template;";
        private MySqlConnection connection;
        public bool SaveAsset(Asset asset)
        {
            MySqlConnection connection;
            int rowsAffected = 0;
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "insert into assets(userid, name, description, value) values("
                    + asset.userId + ",\'" + asset.name + "\',\'" + asset.description + "\'," + asset.value + ")";
                rowsAffected = cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;

            }
            return rowsAffected == 1;
        }


        public int Login(string user, string password)
        {

            List<User> managers = new List<User>();

            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "select * from users where username=\'" + user + "\' and password = \'" + password + "\'";
                //Debug.WriteLine("select * from manager where username=\'" + user + "\' and password = \'" + password + "\'");
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    User manager = new User();
                    manager.id = myreader.GetInt32("id");
                    managers.Add(manager);
                }
                //Debug.WriteLine(managers.Count);
                myreader.Close();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return -1;
            }
            if (managers.Count == 1)
            {
                Debug.WriteLine(managers.ElementAt(0).username);
                Debug.WriteLine(managers.ElementAt(0).id);
                return managers.ElementAt(0).id;
            }
            else {
                return -1;
            }

        }

        public List<Asset> GetAllAssetsOfUser(int userId)
        {
            List<Asset> assets = new List<Asset>();

            try
            {
                connection = new MySqlConnection();

                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * from assets where userid = " + userId;
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Asset asset = new Asset();
                    asset.id = myreader.GetInt32("id");
                    asset.userId = myreader.GetInt32("userid");
                    asset.name = myreader.GetString("name");
                    asset.description = myreader.GetString("description");
                    asset.value = myreader.GetInt32("value");
                    assets.Add(asset);
                }
                myreader.Close();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return assets;
        }


        //    public bool deleteStudent(int studentId)
        //    {
        //        MySql.Data.MySqlClient.MySqlConnection connection;
        //        int rowsAffected = 0;
        //        try
        //        {
        //            connection = new MySql.Data.MySqlClient.MySqlConnection();
        //            connection.ConnectionString = connectionString;
        //            connection.Open();

        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandText = "delete from students where id = " + studentId;
        //            rowsAffected = cmd.ExecuteNonQuery();
        //            connection.Close();
        //        }
        //        catch (MySql.Data.MySqlClient.MySqlException ex)
        //        {
        //            Console.Write(ex.Message);
        //            return false;
        //        }
        //        return rowsAffected == 1;
        //    }


        //    public bool lendBook(int borrowerId, int id)
        //    {
        //        MySql.Data.MySqlClient.MySqlConnection connection;
        //        int rowsAffected = 0;
        //        try
        //        {
        //            connection = new MySql.Data.MySqlClient.MySqlConnection();
        //            connection.ConnectionString = connectionString;
        //            connection.Open();

        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandText = "UPDATE books SET borrower_id = " + borrowerId + " where id = " + id;
        //            rowsAffected = cmd.ExecuteNonQuery();
        //            connection.Close();
        //        }
        //        catch (MySql.Data.MySqlClient.MySqlException ex)
        //        {
        //            Console.Write(ex.Message);
        //            return false;
        //        }
        //        return rowsAffected == 1;

        //    }

    }
}