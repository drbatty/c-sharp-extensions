using System;

namespace CSharpExtensions.DependencyInjection.Interfaces
{
    public interface ITimeService
    {
        DateTime Now();
        DateTime Today();
    }
}