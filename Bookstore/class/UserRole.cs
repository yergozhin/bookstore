using System;

namespace Bookstore.@class
{
    [Flags]
    public enum UserRole
    {
        None      = 0,
        Customer  = 1 << 0,
        Employee  = 1 << 1
        
    }
}