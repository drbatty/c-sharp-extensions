using System;
using CSharpExtensions.DependencyInjection.Interfaces;

namespace CSharpExtensions.DependencyInjection
{
    public class SystemTimeService : ITimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime Today()
        {
            return DateTime.Today;
        }
    }
}