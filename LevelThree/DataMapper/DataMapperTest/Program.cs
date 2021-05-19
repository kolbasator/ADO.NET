using System;
using DataMapper;

namespace DataMapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Movie movie = new Movie(21, "Razumovsky", 1994, 27, 27);
            MovieMapper mapper = new MovieMapper();
            mapper.Save(movie);
            mapper.Insert();
            mapper.Read();
            mapper.Update(22, "Razumovsky Top!", 1994, 27, 27);
            mapper.Read();
            mapper.Delete();
            movie.MovieId = 21;
            movie.Year = 2021;
            movie.Title = "Razumovsky eats potatoes";
            movie.Price = 100;
            movie.AgeRestriction= 12;
            mapper.Insert(); 
            mapper.Read();
            Console.Read();
            Copy copy = new Copy(21,true,5);
            CopyMapper copyMapper = new CopyMapper();
            copyMapper.Save(copy);
            copyMapper.Insert();
            copyMapper.Read();
            copyMapper.Update(22,false,8);
            copyMapper.Read();
            copyMapper.Delete();
            copy.MovieId = 6;
            copy.Available = true;
            copy.CopyId = 23;
            copyMapper.Insert();
            copyMapper.Read();
            Console.Read();
        }
    }
}
