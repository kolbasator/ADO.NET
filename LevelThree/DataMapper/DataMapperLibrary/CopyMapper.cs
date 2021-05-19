using System;
using Npgsql;

namespace DataMapper
{
    public class CopyMapper : ICopyMapper
    { 
        private ICopy _instance { get; set; }
        public void Delete()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand delete = new NpgsqlCommand($"DELETE FROM copies WHERE copy_id={_instance.CopyId} AND available={_instance.Available} AND movie_id={_instance.MovieId}", connection);
                connection.Open();
                delete.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Update(int copyId, bool available, int movieId)
        {
            using(NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand update = new NpgsqlCommand($"UPDATE copies  SET copy_id={copyId},available={available},movie_id={movieId} WHERE copy_id={_instance.CopyId} AND available={_instance.Available} AND movie_id={_instance.MovieId}", connection);
                connection.Open();
                update.ExecuteNonQuery();
                _instance.MovieId = movieId;
                _instance.CopyId = copyId;
                _instance.Available = available;
                connection.Close();
            }
        }
        public void Read()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand read = new NpgsqlCommand($"SELECT * FROM copies WHERE copy_id={_instance.CopyId} AND available={_instance.Available} AND movie_id={_instance.MovieId}", connection);
                connection.Open();
                NpgsqlDataReader reader = read.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"CopyID-{reader[0]},Available-{reader[1]},MovieID-{reader[2]}");
                    }
                } 
                connection.Close(); 
            }
        }
        public void Save(ICopy entity)
        {
            _instance = entity;
        }
        public void Insert()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand insert = new NpgsqlCommand($"INSERT INTO copies VALUES ({_instance.CopyId},{_instance.Available},{_instance.MovieId})",connection);
                connection.Open();
                insert.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
