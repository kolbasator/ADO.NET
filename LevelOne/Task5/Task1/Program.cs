using System;
using Npgsql;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        { 
            NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;");
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT DISTINCT title,available FROM rentals r JOIN copies cop ON cop.copy_id = r.copy_id JOIN movies m ON available = true", connection);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"Title-{reader[0]},Available-{reader[1]}\n\n");
            connection.Close();
        }
    }
}
