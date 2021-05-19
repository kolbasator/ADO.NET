using System;
using Npgsql;
using System.Collections.Generic;

namespace ActiveRecordLibrary
{
    public class MovieRecord
    {
        public int MovieId { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public float Price { get; private set; }
        public int NumberOfAvailableCopies => GetCountOfAvailableCopies();
        public int NumberOfAllCopies => GetCountOfAllCopies();
        public string AvailableACopiesFromTotalCopies => $"{NumberOfAvailableCopies}/{NumberOfAllCopies}";
        public MovieRecord(int movieId, string title, int year, float price)
        {
            MovieId = movieId;
            Title = title;
            Year = year;
            Price = price;
        }
        public void RemoveThisMovieFromDatabase()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand check = new NpgsqlCommand($"SELECT COUNT(movie_id) FROM movies WHERE movie_id={MovieId}");
                int count = int.Parse(Convert.ToString(check.ExecuteReader()[0]));
                if (count != 0)
                {
                    NpgsqlCommand remove = new NpgsqlCommand($"DELETE FROM movies WHERE movie_id={MovieId}");
                    remove.ExecuteNonQuery();
                }
                else
                    Console.WriteLine("Sorry.There aren't any movies in the database with this identifier.");
            }
        }
        public void Rent(int clientId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                if (NumberOfAvailableCopies != 0)
                {
                    NpgsqlCommand getMaxId = new NpgsqlCommand($"SELECT MAX(copy_id) FROM copies WHERE movie_id={MovieId} AND available=true");
                    NpgsqlDataReader reader = getMaxId.ExecuteReader();
                    int copyId = int.Parse(Convert.ToString(reader[0]));
                    NpgsqlCommand insert = new NpgsqlCommand($"INSERT INTO rentals(copy_id, client_id, date_of_rental, date_of_return) VALUES({copyId}, {clientId}, NOW(), NULL)");
                    insert.ExecuteNonQuery();
                    NpgsqlCommand update = new NpgsqlCommand($"UPDATE copies SET available = false WHERE copy_id={copyId}");
                    update.ExecuteNonQuery();
                }
                else
                    Console.WriteLine("Sorry , we haven't  any available copies of this movie.");
            }
        }
        private int GetCountOfAvailableCopies()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand count = new NpgsqlCommand($"SELECT COUNT(copy_id) FROM copies WHERE movie_id={MovieId} AND available=true");
                NpgsqlDataReader reader = count.ExecuteReader();
                int result = int.Parse(Convert.ToString(reader[0]));
                return result;
            }
        }
        private int GetCountOfAllCopies()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand count = new NpgsqlCommand($"SELECT COUNT(copy_id) FROM copies WHERE movie_id={MovieId}");
                NpgsqlDataReader reader = count.ExecuteReader();
                int result = int.Parse(Convert.ToString(reader[0]));
                return result;
            }
        }
        public void InsertThisCopyToDatabase()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand insert = new NpgsqlCommand($"INSERT INTO movies (movie_id,title,year,price) VALUES({MovieId},'{Title}',{Year},{Price})");
                insert.ExecuteNonQuery();
            }
        }
        public static MovieRecord GetMovieById(int id)
        {
            MovieRecord result = null;
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand select = new NpgsqlCommand($"SELECT movie_id,title,year,price FROM movies WHERE copy_id={id}", connection);
                NpgsqlDataReader reader = select.ExecuteReader();
                List<string> movieData = new List<string>();
                int i = 0;
                while (reader.Read())
                {
                    i++;
                    movieData.Add(Convert.ToString(reader[i]));
                }
                result = new MovieRecord(int.Parse(movieData[0]), Convert.ToString(movieData[1]), int.Parse(movieData[2]), float.Parse(movieData[2]));
            }
            return result;
        }
    }
}
