using System.Collections;
using System.Collections.Generic;

namespace Zippable
{
    public class Zippable<T> : IZippable<T>
    {
        private readonly IEnumerable<T> enumerable;

        public Zippable(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return enumerable.GetEnumerator();
        }
    }
}
