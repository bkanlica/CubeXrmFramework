// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="Setting.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Cube.XRM.Framework.Interfaces;
using System;
using System.Xml.Serialization;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework
{
    /// <summary>
    /// Class Setting.
    /// </summary>
    [Serializable]
    public class Setting : ISetting
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        [XmlElement("Key")]
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [XmlElement("Value")]
        public object Value { get; set; }
    }
}
