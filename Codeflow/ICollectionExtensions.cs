using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{
    internal static class ICollectionExtensions
    {
        public static void AddRange<TValue>(this ICollection<TValue> p_Collection, IEnumerable<TValue> p_Values)
        {
            foreach (var l_Value in p_Values)
            {
                p_Collection.Add(l_Value);
            }
        }
    }
}
