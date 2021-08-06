using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace test2
{
    public class CRUD
    {
        public static void create(User user)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=usersDB.db;Version=3;"))
            {
                connection.Execute("INSERT INTO users (phone, email, password) VALUES (@phone, @email, @password)", user);
            }
        }

        public static User read(User user)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=usersDB.db;Version=3;"))
            {
                var output = connection.Query<User>("SELECT * FROM users WHERE phone = @phone and password = @password",user);
                List<User> list = output.ToList();
                if (list.Count == 0) return null;
                else return list.ElementAt(0);
            }
        }

        public static User readById(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=usersDB.db;Version=3;"))
            {
                var output = connection.Query<User>("SELECT * FROM users WHERE id = "+id, new DynamicParameters());
                List<User> list = output.ToList();
                if (list.Count == 0) return null;
                else return list.ElementAt(0);
            }
        }

        public static void update(User user)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=usersDB.db;Version=3;"))
            {
                connection.Execute("UPDATE users SET phone = @phone, email = @email, password = @password WHERE id = @id", user);
            }
        }

        public static void delete(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=usersDB.db;Version=3;"))
            {
                connection.Execute("DELETE FROM users WHERE id = "+id, new DynamicParameters());
            }
        }
    }
}
