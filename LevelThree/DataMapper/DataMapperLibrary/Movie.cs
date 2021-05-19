using System;

namespace DataMapper
{
    public class Movie : IMovie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }
        public Movie(int movieId, string title, int year, int ageRestriction, float price)
        {
            MovieId = movieId;
            Title = title;
            Year = year;
            AgeRestriction = ageRestriction;
            Price = price;
        }
    }
}
