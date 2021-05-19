using System;
using Npgsql;

namespace DataMapper
{
    public class MovieMapper : IMovieMapper
    {
        private IMovie _instance { get; set; }
        public void Delete()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand delete = new NpgsqlCommand($"DELETE FROM movies WHERE movie_id={_instance.MovieId} AND title='{_instance.Title}' AND year={_instance.Year}", connection);
                connection.Open();
                delete.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Update(int movieId, string title, int year, int ageRestriction, float price)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand update = new NpgsqlCommand($"UPDATE movies  SET movie_id={movieId},title='{title}',year={year},age_restriction={ageRestriction},price={price} WHERE movie_id={_instance.MovieId} AND title='{_instance.Title}' AND year={_instance.Year}", connection);
                connection.Open(); 
                update.ExecuteNonQuery();
                _instance.MovieId = movieId;
                _instance.Year = year;
                _instance.Title = title;
                _instance.MovieId = movieId;
                connection.Close();
            }
        }
        public void Read()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand read = new NpgsqlCommand($"SELECT * FROM movies WHERE movie_id={_instance.MovieId}", connection);
                connection.Open(); 
                NpgsqlDataReader reader = read.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID-{reader[0]},Title-{reader[1]},Year-{reader[2]},Age restriction-{reader[3]},Price-{reader[4]}"); 
                    }
                }
                connection.Close(); 
            }
        }
        public void Save(IMovie entity)
        {
            _instance = entity;
        }
        public void Insert()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand insert = new NpgsqlCommand($"INSERT INTO movies VALUES ({_instance.MovieId},'{_instance.Title}',{_instance.Year},{_instance.AgeRestriction},{_instance.Price})", connection);
                connection.Open();
                insert.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
