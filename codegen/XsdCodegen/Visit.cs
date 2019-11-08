using System;
using System.Collections;
using System.Linq;

namespace XsdCodegen
{
    public static class Visit
    {
        public delegate bool ShouldRecurse(object parent, object target);

        /// <summary>
        /// Use reflection to recursively invoke <paramref name="callback"/> on
        /// each property within <paramref name="target"/>
        /// </summary>
        public static void OfType<T>(object target, Action<T> callback)
        {
            OfType(target, callback, DefaultRecursionCheck);
        }

        private static bool DefaultRecursionCheck(object parent, object target)
        {
            return typeof(IEnumerable).IsAssignableFrom(target.GetType()) ||
                parent.GetType().Assembly == target.GetType().Assembly;
        }

        public static void OfType<T>(object target, Action<T> callback, ShouldRecurse shouldRecurse)
        {
            if (target == null)
            {
                return;
            }

            foreach (var prop in target.GetType().GetProperties())
            {
                var value = prop.GetValue(target);

                if (value == null || !shouldRecurse(target, value))
                {
                    continue;
                }

                if (value is IEnumerable collection)
                {
                    foreach (var item in collection)
                    {
                        if (item is T itemTee)
                        {
                            callback(itemTee);
                        }
                        OfType(item, callback, shouldRecurse);
                    }
                }
                else
                {
                    OfType(value, callback, shouldRecurse);
                }
            }
        }

        private static IEnumerable Flatten(object thing)
        {
            if (thing is IEnumerable collection)
            {
                return collection;
            }
            else
            {
                return Enumerable.Repeat(thing, 1);
            }
        }
    }
}