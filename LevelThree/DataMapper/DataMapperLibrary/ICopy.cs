using System;

namespace DataMapper
{
    public interface ICopy
    { 
        int CopyId { get; set; }
        bool Available { get; set; }
        int MovieId { get; set; } 
    }
}
