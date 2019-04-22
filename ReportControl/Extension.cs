using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public static class Extension
{
    public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
    {
        if (sequences == null)
        {
            return null;
        }

        IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };

        return sequences.Aggregate(
            emptyProduct,
            (accumulator, sequence) => accumulator.SelectMany(
                accseq => sequence,
                (accseq, item) => accseq.Concat(new[] { item })));
    }

}
