using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.ComponentModel;

namespace Bilet_24
{
    internal class DBHelper
    {
        private static MySqlConnection? conn = null;
        private DBHelper(
            String host,
            int port,
            String user,
            String password,
            String database
            )
        {
            // Строка подключения
            var connStr = $"Server={host};port={port};database={database};User Id={user};password={password}";
            conn = new MySqlConnection(connStr);
            // Открываем строку подключения
            conn?.Open();
        }

        private static DBHelper instance = null;
        public static DBHelper GetInstance(
                        String host = "localhost",
                        int port = 0,
                        String user = "root",
                        String password = "",
                        String database = ""
                        )
        {
            if (instance == null)
            {
                instance = new DBHelper(host, port, user, password, database);
            }
            return instance;
        }

        public List<double> SelectElems(int user_id)
        {
            string res = " ";
            var queryStr = $"SELECT `elems` FROM `deter` WHERE `user_id`=@user_id";

            // Создание команды
            var cmd = conn?.CreateCommand();
            cmd.CommandText = queryStr;
            cmd.Parameters.AddWithValue("@user_id", user_id);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        res = reader[0].ToString();
                    }
                }
            }
            return res.Split(" ").Select(x=>double.Parse(x)).ToList();
        }

        public string SelectRank(int user_id)
        {
            string res = " ";
            var queryStr = $"SELECT `rank` FROM `deter` WHERE `user_id`=@user_id";

            // Создание команды
            var cmd = conn?.CreateCommand();
            cmd.CommandText = queryStr;
            cmd.Parameters.AddWithValue("@user_id", user_id);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        res = reader[0].ToString();
                    }
                }
            }
            return res;
        }

        // Запрос на добавление результатa пользователя
        public bool UpdateResult(double _result, int user_id)
        {
            try
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"UPDATE `deter` SET `result` = @result WHERE `deter`.`user_id` = @user_id";
                cmd.Parameters.Add(new MySqlParameter("@result", _result));
                cmd.Parameters.Add(new MySqlParameter("@user_id", user_id));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Запрос на добавление данных не выполнен");
                return false;
            }
        }
    }
}
