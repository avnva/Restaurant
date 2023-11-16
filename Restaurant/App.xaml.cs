using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BCrypt.Net;
using Npgsql;

namespace Restaurant;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        //string username = "admin";
        //string password = "admin";

        //// Хэширование пароля
        //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        //using (var connection = new NpgsqlConnection("Host = localhost; Port = 5432; Database = restaurantdb; Username = admin; Password = admin"))
        //{
        //    connection.Open();

        //    // SQL запрос для вставки пользователя в базу данных
        //    string sql = "INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)";

        //    using (var cmd = new NpgsqlCommand(sql, connection))
        //    {
        //        // Передаем параметры запроса
        //        cmd.Parameters.AddWithValue("Username", username);
        //        cmd.Parameters.AddWithValue("PasswordHash", hashedPassword);

        //        // Выполняем запрос
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //Console.WriteLine("Пользователь успешно добавлен!");
    }
}
