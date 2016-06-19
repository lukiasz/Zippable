using System;
using System.Collections.Generic;

namespace FunFaker
{
    /// <summary>
    /// Replacement for original, Linq SelectMany method for convinient generating of
    /// fake entities. It zips collections.
    /// </summary>
    public static class ZippableExtensions
    {
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IZippable<TSource> source,
            Func<TSource, IZippable<TCollection>> selector, Func<TSource, TCollection, TResult> select)
        {
            return SelectManyImpl(source, (value, index) => selector(value), select);
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IZippable<TSource> source,
            Func<TSource, IZippable<TResult>> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return SelectManyImpl(source, (value, index) => selector(value),
                (originalElement, subsequenceElement) => subsequenceElement);
        }

        /// <summary>
        /// Actual code for SelectMany implementation. Instead of doing Cartesian Product, it
        /// performs zipping.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="collectionSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> SelectManyImpl<TSource, TCollection, TResult>(IZippable<TSource> source,
            Func<TSource, int, IZippable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            int index = 0;

            using (var e1 = source.GetEnumerator())
            {
                // a default(TSource) causes that using nested "from" in LINQ won't work. Example of what's not allowed:
                // from entity in entities
                // from element in entity.collection <- this won't work
                // ....
                using (var e2 = collectionSelector(default(TSource), index++).GetEnumerator())
                {
                    while (true)
                    {
                        var isNext1 = e1.MoveNext();
                        var isNext2 = e2.MoveNext();
                        if (isNext1 && isNext2)
                            yield return resultSelector(e1.Current, e2.Current);
                        else
                            yield break;
                    }
                }
            }
        }
    }
}
