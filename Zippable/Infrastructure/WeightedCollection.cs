using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFaker
{
    public class WeightedCollection<T> : IReadOnlyCollection<T>
    {
        private readonly SortedList<double, T> collection;
        private readonly Random random;

        public WeightedCollection(IReadOnlyCollection<T> collection,
            Func<T, double> weightFunction, Random random)
        {
            this.random = random;
            this.collection = new SortedList<double, T>();

            foreach (var e in collection)
                this.collection.Add(weightFunction(e), e);
        }

        public int Count
        {
            get
            {
                return collection.Count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                var max = collection.Keys[Count - 1];

                var randomNumber = random.NextDouble(0, max);
                foreach (var key in collection.Keys)
                {
                    if (randomNumber <= key)
                        yield return collection[key];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
