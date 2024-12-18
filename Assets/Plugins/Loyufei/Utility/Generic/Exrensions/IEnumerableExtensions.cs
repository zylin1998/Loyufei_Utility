using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self) { action?.Invoke(item); }
        }

        #region First or Default

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> self, TSource isDefault) 
        {
            return self.FirstOrDefault() ?? isDefault;
        }

        public static TSource FirstOrDefault<TSource>(
            this IEnumerable<TSource> self, 
                 Func<TSource, bool>  predicate,
                 TSource              isDefault)
        {  
            return self.FirstOrDefault(predicate) ?? isDefault;
        }

        #endregion
    }
}
