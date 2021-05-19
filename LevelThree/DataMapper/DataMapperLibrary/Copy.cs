using System;

namespace DataMapper
{
    public class Copy : ICopy
    { 
        public int CopyId { get; set; }
        public bool Available { get; set; }
        public int MovieId { get; set; } 
        public Copy(int copyId,bool available,int movieId)
        {
            CopyId = copyId;
            Available = available;
            MovieId = movieId;
        }
    }
}
