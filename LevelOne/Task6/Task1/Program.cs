using System;
using Npgsql;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;");
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM copies", connection);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine($"Copy id-{reader[0]},Available-{reader[1]},Movie id-{reader[2]}\n\n");
                connection.Close();
                Console.WriteLine("Do you want to make a new copy?Yes-1,No-0");
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    NpgsqlConnection addConnection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;");
                    addConnection.Open();
                    Console.Write("Movie id:");
                    int movieId = Convert.ToInt32(Console.Read());
                    Console.Write("Copy id:");
                    int copyId = Convert.ToInt32(Console.Read()); 
                    Console.Write("Available:");
                    bool available = Convert.ToBoolean(Console.Read());
                    addConnection.Open(); 
                    NpgsqlCommand add =new NpgsqlCommand($"INSERT INTO copies (copy_id,available,movie_id) VALUES({copyId},{available},{movieId})", addConnection); 
                    add.ExecuteNonQuery();
                    Console.Read();
                    addConnection.Close();
                    Console.Clear();
                    continue;
                } 
                Console.Clear();
                continue;
            } 
        }
    }
}
