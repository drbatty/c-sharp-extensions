using CSharpExtensions.DependencyInjection.Interfaces;

namespace CSharpExtensions.DependencyInjection
{
    public static class Providers
    {
        public static ITimeService TimeService { get; set; }
        public static IRandomisationService RandomisationService { get; set; }
    }
}