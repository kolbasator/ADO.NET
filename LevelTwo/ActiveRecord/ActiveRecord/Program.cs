using System;
using Npgsql;

namespace ActiveRecord 
{
    class Program
    {
        static void Main(string[] args)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT m.movie_id,title,year,age_restriction,price,COUNT(cop.copy_id) AS count FROM movies m JOIN copies cop ON cop.copy_id=m.movie_id GROUP BY m.movie_id");
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"Movie id-{reader[0]},Title-{reader[1]},Year-{reader[2]},Age restriction-{reader[3]},Price-{reader[4]},Count of copies-{reader[5]}");
            conn.Close();
        }
    }
}
