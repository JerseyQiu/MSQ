using System.Collections.Generic;
using Impac.Mosaiq.IQ.Extensions.Interface;

namespace Impac.Mosaiq.IQ.Extensions.Client
{
    ///<summary>Default implementation of IRuntimeValueCache.
    ///</summary>
    public class RuntimeValueCacheExtension : IRuntimeValueCache
    {
        #region Static Fields
        private static readonly Dictionary<string, string> ValuesDict = new Dictionary<string, string>();
        private static readonly object LockObj = new object();
        #endregion

        #region Static Methods
        /// <summary> Clears all values in the values dictionary. </summary>
        public static void ClearValues()
        {
            lock (LockObj)
                ValuesDict.Clear();
        }
        #endregion

        #region IRuntimeValueCache Methods
        /// <summary>
        /// Gets a value from the cache.
        /// </summary>
        /// <param name="key">The key of the value to be found</param>
        /// <param name="value">The value saved for the key entered</param>
        /// <returns>True if a value was found.  False otherwise.</returns>
        public bool TryGetRuntimeValue(string key, out string value)
        {
            return ValuesDict.TryGetValue(key, out value);
        }

        /// <summary>
        /// Sets a runtime value based on the key provided.  A value with a matching key already in the
        /// cache will be overwritten.
        /// </summary>
        /// <param name="key">The key of the value to be stored.</param>
        /// <param name="value">The value to be stored.</param>
        public void SetRuntimeValue(string key, string value)
        {
            lock (LockObj)
            {
                if (ValuesDict.ContainsKey(key))
                    ValuesDict.Remove(key);

                ValuesDict.Add(key, value);
            }
        }
        #endregion
    }
}
