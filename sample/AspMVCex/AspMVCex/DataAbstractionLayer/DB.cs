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

        //public Student selectStudent(string name)
        //{
        //    try
        //    {
        //        connection = new MySql.Data.MySqlClient.MySqlConnection();
        //        connection.ConnectionString = connectionString;
        //        connection.Open();

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandText = "select * from students where name=\'" + name + "\'";
        //        MySqlDataReader myreader = cmd.ExecuteReader();

        //        Student student = new Student();
        //        if (myreader.Read())
        //        {
        //            student.id = myreader.GetInt32("id");
        //            student.name = myreader.GetString("name");
        //            student.groupId = myreader.GetInt32("group_id");
        //        }
        //        myreader.Close();
        //        connection.Close();
        //        return student;
        //    }
        //    catch (MySql.Data.MySqlClient.MySqlException ex)
        //    {
        //        Console.Write(ex.Message);
        //        return null;
        //    }

        //}

        //public Student selectStudent(int id)
        //{
        //    try
        //    {
        //        connection = new MySql.Data.MySqlClient.MySqlConnection();
        //        connection.ConnectionString = connectionString;
        //        connection.Open();

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandText = "select * from students where id=" + id;
        //        MySqlDataReader myreader = cmd.ExecuteReader();

        //        Student student = new Student();
        //        if (myreader.Read())
        //        {
        //            student.id = myreader.GetInt32("id");
        //            student.name = myreader.GetString("name");
        //            student.groupId = myreader.GetInt32("group_id");
        //        }
        //        myreader.Close();
        //        connection.Close();
        //        return student;
        //    }
        //    catch (MySql.Data.MySqlClient.MySqlException ex)
        //    {
        //        Console.Write(ex.Message);
        //        return null;
        //    }

        //}

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


        //    public List<Book> selectBooksWithIdentifierLike(string identifier, string title)
        //    {
        //        List<Book> books = new List<Book>();

        //        try
        //        {
        //            connection = new MySql.Data.MySqlClient.MySqlConnection();
        //            connection.ConnectionString = connectionString;
        //            connection.Open();

        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandText = "SELECT * from books where " + identifier + " LIKE \'%" + title + "%\'";
        //            MySqlDataReader myreader = cmd.ExecuteReader();

        //            while (myreader.Read())
        //            {
        //                Book book = new Book();
        //                book.id = myreader.GetInt32("id");
        //                book.author = myreader.GetString("author");
        //                book.title = myreader.GetString("title");
        //                book.publisher = myreader.GetString("publisher");
        //                book.genre = myreader.GetString("genre");
        //                try { 
        //                book.borrowerId = myreader.GetInt32("borrower_id");
        //                }
        //                catch (SqlNullValueException e)
        //                {
        //                    book.borrowerId = -1;
        //                }
        //                books.Add(book);
        //            }
        //            myreader.Close();
        //            connection.Close();
        //        }
        //        catch (MySql.Data.MySqlClient.MySqlException ex)
        //        {
        //            Console.Write(ex.Message);
        //        }
        //        return books;
        //    }

        //    public List<Student> getAllStudents()
        //    {

        //        List<Student> students = new List<Student>();

        //        try
        //        {
        //            connection = new MySql.Data.MySqlClient.MySqlConnection();
        //            connection.ConnectionString = connectionString;
        //            connection.Open();

        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandText = "select * from students";
        //            MySqlDataReader myreader = cmd.ExecuteReader();

        //            while (myreader.Read())
        //            {
        //                Student stud = new Student();
        //                stud.id = myreader.GetInt32("id");
        //                stud.name = myreader.GetString("name");
        //                stud.groupId = myreader.GetInt32("group_id");
        //                Console.WriteLine(stud.name);
        //                students.Add(stud);
        //            }
        //            myreader.Close();
        //            connection.Close();
        //        }
        //        catch (MySql.Data.MySqlClient.MySqlException ex)
        //        {
        //            Console.Write(ex.Message);
        //        }
        //        return students;

        //    }

        //    public List<Book> getAllLentBooks()
        //    {
        //        List<Book> books = new List<Book>();

        //        try
        //        {
        //            connection = new MySql.Data.MySqlClient.MySqlConnection();

        //            connection.ConnectionString = connectionString;
        //            connection.Open();

        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandText = "SELECT b.id as bid, b.author as bauthor, b.title as btitle, b.publisher as bpublisher, b.genre as bgenre, b.borrower_id as bborrower_id FROM books b left join students s on b.borrower_id = s.id where b.borrower_id is not NULL";
        //            MySqlDataReader myreader = cmd.ExecuteReader();

        //            while (myreader.Read())
        //            {
        //                Book book = new Book();
        //                book.id = myreader.GetInt32("bid");
        //                book.author = myreader.GetString("bauthor");
        //                book.title = myreader.GetString("btitle");
        //                book.publisher = myreader.GetString("bpublisher");
        //                book.genre = myreader.GetString("bgenre");
        //                try { 
        //                book.borrowerId = myreader.GetInt32("bborrower_id");
        //                }
        //                catch (SqlNullValueException e)
        //                {
        //                    book.borrowerId = -1;
        //                }
        //                books.Add(book);
        //            }
        //            myreader.Close();
        //            connection.Close();
        //        }
        //        catch (MySql.Data.MySqlClient.MySqlException ex)
        //        {
        //            Console.Write(ex.Message);
        //        }
        //        return books;
        //    }

        //    public List<Book> getAllAvailableBooks()
        //    {
        //        List<Book> books = new List<Book>();

        //        try
        //        {
        //            connection = new MySql.Data.MySqlClient.MySqlConnection();

        //            connection.ConnectionString = connectionString;
        //            connection.Open();

        //            MySqlCommand cmd = new MySqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandText = "SELECT b.id as bid, b.author as bauthor, b.title as btitle, b.publisher as bpublisher, b.genre as bgenre, b.borrower_id as bborrower_id FROM books b left join students s on b.borrower_id = s.id where b.borrower_id is NULL";
        //            MySqlDataReader myreader = cmd.ExecuteReader();

        //            while (myreader.Read())
        //            {
        //                Book book = new Book();
        //                book.id = myreader.GetInt32("bid");
        //                book.author = myreader.GetString("bauthor");
        //                book.title = myreader.GetString("btitle");
        //                book.publisher = myreader.GetString("bpublisher");
        //                book.genre = myreader.GetString("bgenre");
        //                try { 
        //                book.borrowerId = myreader.GetInt32("bborrower_id");
        //                                        }
        //                catch (SqlNullValueException e)
        //                {
        //                    book.borrowerId = -1;
        //                }
        //                books.Add(book);
        //            }
        //            myreader.Close();
        //            connection.Close();
        //        }
        //        catch (MySql.Data.MySqlClient.MySqlException ex)
        //        {
        //            Console.Write(ex.Message);
        //        }
        //        return books;
        //    }


        //    public bool saveBook(Book book)
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
        //            cmd.CommandText = "insert into books(author, title, publisher, genre, borrower_id) values(\'"
        //                + book.author + "\',\'" + book.title + "\',\'" + book.publisher + "\',\'" + book.genre + "\', NULL)";
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

        //    public bool saveStudent(Student student)
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
        //            cmd.CommandText = "insert into students(name, group_id) values(\'" + student.name + "\'," + student.groupId + ")";
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

        //    public bool deleteBook(int bookid)
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
        //            cmd.CommandText = "delete from books where id = " + bookid;
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


        //    public bool returnBook(int id)
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
        //            cmd.CommandText = "UPDATE books SET borrower_id = NULL where id = " + id;
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

        //    public bool updateStudent(int id, int group, string name)
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
        //            cmd.CommandText = "UPDATE students SET name = \'" + name + "\', group_id = " + group + " where id = " + id;
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

        //    public bool updateBook(int id, string author, string title, string publisher, string genre)
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
        //            cmd.CommandText = "UPDATE books SET author = \'" + author + "\', title = \'" + title + "\', publisher = \'" +
        //                publisher + "\', genre = \'" + genre + "\' where id = " + id;
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