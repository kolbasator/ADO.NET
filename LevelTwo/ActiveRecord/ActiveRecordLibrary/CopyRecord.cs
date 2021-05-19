using System;
using Npgsql;
using System.Collections.Generic;

namespace ActiveRecordLibrary
{
    public class CopyRecord
    { 
        public int CopyId { get; private set; }
        public int MovieId { get; private set; }
        public bool Available { get; private set; }
        public CopyRecord(int copyId, int movieId, bool available)
        {
            CopyId = copyId;
            MovieId = movieId;
            Available = available;
        }
        public void RemoveThisCopyFromDatabase()
        {
            using(NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand check = new NpgsqlCommand($"SELECT COUNT(copy_id) FROM copies WHERE copy_id={CopyId}");
                int count = int.Parse(Convert.ToString(check.ExecuteReader()[0]));
                if (count != 0)
                {
                    NpgsqlCommand remove = new NpgsqlCommand($"DELETE FROM copies WHERE copy_id={CopyId} AND available={Available} AND movie_id={MovieId}");
                    remove.ExecuteNonQuery();
                }
                else
                    Console.WriteLine("Sorry.There aren't any copies in the database with this identifier.");
            }
        }
        public void InsertThisCopyToDatabase()
        {
            using(NpgsqlConnection connection=new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand insert = new NpgsqlCommand($"INSERT INTO copies (copy_id,available,movie_id) VALUES({CopyId},{Available},{MovieId})");
                insert.ExecuteNonQuery();
            }
        }
        public static CopyRecord GetCopyById(int id)
        {
            CopyRecord result = null;
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres; Password = razumovsky123; Database = MoviesCodeFirst;"))
            {
                NpgsqlCommand select = new NpgsqlCommand($"SELECT copy_id,available,movie_id FROM copies WHERE copy_id={id}",connection);
                NpgsqlDataReader reader = select.ExecuteReader();
                List<string> copyData = new List<string>();
                int i = 0;
                while (reader.Read())
                {
                    i++;
                    copyData.Add(Convert.ToString(reader[i]));
                }
                result = new CopyRecord(int.Parse(copyData[0]), int.Parse(copyData[1]), bool.Parse(copyData[2]));
            }
            return result;
        } 
    }
}
