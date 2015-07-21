using Pelorus.Core.Reflection;
using System;
using System.Linq;
using System.Reflection;

namespace Pelorus.Core.Localization
{
    /// <summary>
    /// Localizes the DateTime values using a delegate method.
    /// TODO: This class is very incomplete and limited.
    /// </summary>
    public class LocalizeCore
    {
        /// <summary>
        /// Traverse the input object recursively and execute the localizer method on all properties of type DateTime.  All elements of arrays and collections that implement an 'int' 
        /// indexer will be examined.
        /// </summary>
        /// <param name="obj">Object to traverse</param>
        /// <param name="localizer">Method to perform the localization on the DateTime properties</param>
        /// <returns>Traversed object</returns>
        public object TraverseObject(object obj, LocalizerDelegate localizer)
        {
            if (null == obj)
            {
                return null;
            }

            var objectType = obj.GetType();

            if (typeof (DateTime) == objectType)
            {
                return localizer((DateTime) obj);
            }

            if (objectType.IsArray)
            {
                var array = (Array) obj;

                for (int i = 0; i < array.Length; i++)
                {
                    var element = array.GetValue(i);
                    var localizedObject = this.TraverseObject(element, localizer);
                    array.SetValue(localizedObject, i);
                }

                return obj;
            }

            if (objectType.HasIndexer<int>())
            {
                var listType = objectType.GetGenericArguments()
                                         .FirstOrDefault();

                if (null == listType)
                {
                    return obj;
                }

                try
                {
                    int i = 0;
                    while (true)
                    {
                        var indexer = objectType.GetIndexer<int>();
                        var indexValue = indexer.GetValue(obj, new object[] { i });

                        var localizedObject = this.TraverseObject(indexValue, localizer);
                        indexer.SetValue(obj, localizedObject, new object[] { i });
                        i++;
                    }
                }
                catch (TargetInvocationException ex)
                {
                    if (false == (ex.InnerException is ArgumentOutOfRangeException))
                    {
                        throw;
                    }

                    // The index of the collection was exceeded and execution can continue traversing the properties.
                }

                return obj;
            }

            if (true == objectType.IsSerializable)
            {
                return obj;
            }

            var properties = objectType.GetProperties();

            foreach (var prop in properties)
            {
                if ((false == prop.CanWrite) || (null != prop.GetCustomAttribute<LocalizerIgnoreAttribute>()))
                {
                    continue;
                }

                var value = prop.GetValue(obj, null);

                if (value == null)
                {
                    continue;
                }

                var localizedObject = this.TraverseObject(value, localizer);
                prop.SetValue(obj, localizedObject, null);
            }

            return obj;
        }
    }
}
