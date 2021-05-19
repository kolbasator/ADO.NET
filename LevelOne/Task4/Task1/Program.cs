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
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM copies c JOIN movies m ON m.movie_id=c.copy_id", connection);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"Title-{reader[0]},Movie_id-{reader[1]},Age restriction-{reader[2]},Year-{reader[3]},Firstname-{reader[4]},Lastname-{reader[5]},Copy_id-{reader[6]},Available-{reader[7]}\n\n");
            connection.Close();
        }
    }
}
