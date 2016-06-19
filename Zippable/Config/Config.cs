using System;

namespace FunFaker
{
    /// <summary>
    /// Used for shuffling data in WeightedCollection.
    /// </summary>
    public static class Config
    {
        public static Random Random { get; set; } = new Random();
    }
}
