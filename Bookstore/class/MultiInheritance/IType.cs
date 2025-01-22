using System;

namespace Bookstore.@class
{
    public interface IType
    {
        string TypeName { get; }
    }

    public interface IComics : IType
    {
        int IssueNumber { get; set; }
    }

    public interface IManga : IType
    {
        int Volume { get; set; }
    }
}