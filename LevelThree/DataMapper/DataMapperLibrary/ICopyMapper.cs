using System;

namespace DataMapper
{
    public interface ICopyMapper
    {  
        void Delete();
        void Update(int movieId,bool available,int copyId);
        void Read();
        void Save(ICopy entity);
        void Insert();
    }
}
