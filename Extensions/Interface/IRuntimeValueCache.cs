namespace Impac.Mosaiq.IQ.Extensions.Interface
{
    /// <summary>
    /// This interface defines methods which can be used to save and load values from the runtime value cache.
    /// Runtime Values are used to communicate information between IQ Script executions.
    /// </summary>
    public interface IRuntimeValueCache
    {
        /// <summary>
        /// Gets a value from the cache.
        /// </summary>
        /// <param name="key">The key of the value to be found</param>
        /// <param name="value">The value saved for the key entered</param>
        /// <returns>True if a value was found.  False otherwise.</returns>
        bool TryGetRuntimeValue(string key, out string value);

        /// <summary>
        /// Sets a runtime value based on the key provided.  A value with a matching key already in the
        /// cache will be overwritten.
        /// </summary>
        /// <param name="key">The key of the value to be stored.</param>
        /// <param name="value">The value to be stored.</param>
        void SetRuntimeValue(string key, string value);
    }
}
