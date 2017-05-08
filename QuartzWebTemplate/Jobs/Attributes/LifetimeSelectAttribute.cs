using System;

namespace QuartzWebTemplate.Jobs.Attributes
{
    public class LifetimeSelectAttribute : Attribute
    {
        readonly LifetimeSelection _selection;

        public LifetimeSelectAttribute(LifetimeSelection selection)
        {
            _selection = selection;
        }

        public LifetimeSelection Selection { get { return _selection; } }
    }
}