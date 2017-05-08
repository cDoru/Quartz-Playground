using System;

namespace QuartzWebTemplate.Jobs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DontRegisterAttribute : Attribute
    {
    }
}