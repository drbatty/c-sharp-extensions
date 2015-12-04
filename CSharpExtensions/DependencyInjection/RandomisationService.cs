using System;
using CSharpExtensions.DependencyInjection.Interfaces;

namespace CSharpExtensions.DependencyInjection
{
    public class RandomisationService : IRandomisationService
    {
        private readonly Random _random = new Random();

        public double NextDouble()
        {
            return _random.NextDouble();
        }
    }
}