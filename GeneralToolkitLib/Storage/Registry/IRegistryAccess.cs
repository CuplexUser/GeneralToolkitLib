namespace GeneralToolkitLib.Storage.Registry
{
    /// <summary>
    /// IRegistryAccess interface for real or mock registry
    /// </summary>
    public interface IRegistryAccess
    {
        /// <summary>
        /// Gets or sets a value indicating whether [show error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show error]; otherwise, <c>false</c>.
        /// </value>
        bool ShowError { get; set; }

        /// <summary>
        /// A property to set the SubKey value
        /// (default = "SOFTWARE\\" + Application.ProductName
        /// </summary>
        /// <value>
        /// The sub key.
        /// </value>
        string SubKey { get; }

        /// <summary>
        /// Setups the sub key path and access rights.
        /// </summary>
        void SetupSubKeyPathAndAccessRights();
        /// <summary>
        /// Reads the object from registry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ReadObjectFromRegistry<T>() where T : new();

        /// <summary>
        /// Tries the read object from registry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objFromRegistry">The object from registry.</param>
        /// <returns></returns>
        bool TryReadObjectFromRegistry<T>(out T objFromRegistry) where T : new();
        /// <summary>
        /// Saves the object to registry.
        /// </summary>
        /// <param name="objToSave">The object to save.</param>
        void SaveObjectToRegistry(object objToSave);
        /// <summary>
        /// Deletes the key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        bool DeleteKey(string keyName);

        /// <summary>
        /// To delete a sub key and any child.
        /// input: void
        /// output: true or false
        /// </summary>
        /// <returns></returns>
        bool DeleteSubKeyTree();

        /// <summary>
        /// Retrive the count of subkeys at the current key.
        /// input: void
        /// output: number of subkeys
        /// </summary>
        /// <returns></returns>
        int SubKeyCount();

        /// <summary>
        /// Retrive the count of values in the key.
        /// input: void
        /// output: number of keys
        /// </summary>
        /// <returns></returns>
        int ValueCount();
    }
}