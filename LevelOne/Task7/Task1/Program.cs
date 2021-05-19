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
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM movies", connection);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine($"Movie id-{reader[0]},Title-{reader[1]},Year-{reader[2]},Age restriction-{reader[3]},Price-{reader[4]}\n\n");
                connection.Close();
                Console.WriteLine("Do you want to make a new movie?Yes-1,No-0");
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    NpgsqlConnection addConnection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;");
                    addConnection.Open();
                    Console.Write("Movie id:");
                    int movieId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Title:");
                    string title = Console.ReadLine(); 
                    Console.Write("Year:");
                    int year = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Age Restriction:");
                    int ageRestriction = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Price:");
                    float price = float.Parse(Console.ReadLine()); 
                    NpgsqlCommand add =new NpgsqlCommand($"INSERT INTO movies (movie_id,title,year,age_restriction,price) VALUES({movieId},'{title}',{year},{ageRestriction},{price})", addConnection); 
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
