using System;
using System.Collections.Generic;
using System.Linq;

namespace FunFaker
{
    public static class Extensions
    {
        public static WeightedCollection<T> ToWeightedCollection<T>(this IReadOnlyCollection<T> collection,
            Func<T, double> weightFunction)
        {
            return new WeightedCollection<T>(collection, weightFunction, Config.Random);
        }

        public static IZippable<T> ToZippable<T>(this IEnumerable<T> collection)
        {
            return new Zippable<T>(collection);
        }

        public static IEnumerable<T> PossibleEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> set,
            double probabilityOfEachElement = 0.5)
        {
            var enumerator = set.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (Config.Random.NextDouble() < probabilityOfEachElement)
                    yield return enumerator.Current;
            }
            yield break;
        }

        public static IEnumerable<T> ToModuloSequence<T>(this IEnumerable<T> sequence)
        {
            while (true)
            {
                foreach (var a in sequence)
                {
                    yield return a;
                }
            }
        }

        public static IEnumerable<T> ToRandomInfiniteSequence<T>(this IReadOnlyCollection<T> collection)
        {
            var collection2 = collection.ToList();
            while (true)
            {
                collection2.ShuffleInPlace();

                foreach (var a in collection2)
                {
                    yield return a;
                }
            }
        }

        public static void ShuffleInPlace<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Config.Random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IEnumerable<T> Lift<T>(Func<T> factory)
        {
            while (true)
            {
                yield return factory();
            }
        }

        public static IZippable<T> LiftToZippable<T>(Func<T> factory)
        {
            return Lift(factory).ToZippable();
        }
    }
}
