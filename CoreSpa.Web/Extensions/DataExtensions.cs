using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoreSpa.Web.Extensions
{
    public static class DataExtensions
    {
        private static readonly IDictionary<Type, PropertyInfo[]> MappingPlans = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public static T ApplyChanges<T>(this T item, T changes)
        {
            MappingPlans.TryGetValue(typeof(T), out var properties);

            if (properties == null)
            {
                properties = typeof(T)
                    .GetProperties()
                    .Where(x => x.CanRead && x.CanWrite)
                    .ToArray();

                MappingPlans[typeof(T)] = properties;
            }

            foreach (var property in properties)
            {
                var value = property.GetValue(changes);

                if (value != null)
                {
                    property.SetValue(item, value);
                }
            }

            return item;
        }
    }
}
