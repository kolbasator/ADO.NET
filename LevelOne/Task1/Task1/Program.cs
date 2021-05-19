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
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT title,movie_id FROM movies", connection);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"Title-{reader[0]},Movie_id-{reader[1]}");
            connection.Close();
        }
    }
}
