using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.utility.extension
{
    /// <summary>
    /// as per https://social.technet.microsoft.com/wiki/contents/articles/17556.how-to-query-trees-using-linq.aspx
    /// </summary>
    public static class LinqTreeExtension
    {
        public static IEnumerable<T> SelectNestedChildren<T>
            (this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            foreach (T item in source)
            {
                yield return item;
                foreach (T subItem in selector(item).SelectNestedChildren(selector))
                {
                    yield return subItem;
                }
            }
        }
    }
}
