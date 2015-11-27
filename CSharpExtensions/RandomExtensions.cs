using System;

namespace CSharpExtensions
{
    public static class RandomExtensions
    {
        /// <summary>
        /// returns a boolean which is true with the given probability
        /// </summary>
        /// <param name="random">a random number generator</param>
        /// <param name="probability">the given probability</param>
        /// <returns>a boolean which is true with the given probability</returns>
        public static bool NextBool(this Random random, double probability)
        {
            return random.NextDouble() <= probability;
        }

        /// <summary>
        /// returns a boolean which is true with probability 0.5, like a coin toss
        /// </summary>
        /// <param name="random">a random number generator</param>
        /// <returns>a boolean which is true with probability 0.5</returns>
        public static bool NextBool(this Random random)
        {
            return random.NextDouble() <= 0.5;
        }

        /// <summary>
        /// returns a random choice from zero or more parameters
        /// </summary>
        /// <typeparam name="T">the type of the parameters</typeparam>
        /// <param name="random"></param>
        /// <param name="things">the objects to select from</param>
        /// <returns>a random choice from zero or more parameters</returns>
        public static T OneOf<T>(this Random random, params T[] things)
        {
            return things[random.Next(things.Length)];
        }
    }
}