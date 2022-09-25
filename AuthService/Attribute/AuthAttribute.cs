using System;

namespace AuthService.Attributee
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute
    {
    }
}
