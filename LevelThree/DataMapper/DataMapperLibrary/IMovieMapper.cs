using System;

namespace DataMapper
{
    public interface IMovieMapper
    {  
        void Delete();
        void Update(int movieId,string title,int year,int ageRestriction,float price);
        void Read();
        void Save(IMovie entity);
        void Insert();
    }
}
