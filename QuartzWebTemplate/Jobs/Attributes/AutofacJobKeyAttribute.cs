using System;

namespace QuartzWebTemplate.Jobs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutofacJobKeyAttribute : Attribute
    {
        readonly string _key;

        public AutofacJobKeyAttribute(string key)
        {
            _key = key;
        }

        public string Key { get { return _key; } }
    }
}