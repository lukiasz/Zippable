using System.Collections;
using System.Collections.Generic;

namespace Zippable
{
    public interface IZippable<T> : IEnumerable<T>, IEnumerable { }
}
