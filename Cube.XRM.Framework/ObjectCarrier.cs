// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="ObjectCarrier.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Concurrent;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class ObjectCarrier.
    /// </summary>
    public static class ObjectCarrier
    {
        /// <summary>
        /// The carrier object
        /// </summary>
        static private ConcurrentDictionary<string, object> carrierObject = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Determines whether the specified key has value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the specified key has value; otherwise, <c>false</c>.</returns>
        public static bool HasValue(string key)
        {
            if (!carrierObject.ContainsKey(key))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetValue(string key, object value)
        {
            //var item = new Setting()
            //{
            //    Value = value
            //};

            carrierObject.AddOrUpdate(key, value, (oldKey, oldValue) => { return value; });
        }

        /// <summary>
        /// Gets all values.
        /// </summary>
        /// <returns>ConcurrentDictionary&lt;System.String, System.Object&gt;.</returns>
        public static ConcurrentDictionary<string, object> GetAllValues()
        {
            return carrierObject;       
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public static T GetValue<T>(string key)
        {
            if (HasValue(key))
            {
                return (T)(carrierObject[key]);
            }
            else
            {
                return default(T);
            }
        }

    }
}
