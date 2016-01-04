// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="SettingGroup.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class SettingGroup.
    /// </summary>
    [Serializable]
    public class SettingGroup : ISettingGroup
    {
        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        [XmlElement("Group")]
        public string GroupName { get; set; }
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        [XmlElement("Settings")]
        public List<Setting> Settings { get; set; }
    }

    /// <summary>
    /// Interface ISettingGroup
    /// </summary>
    public interface ISettingGroup
    {
        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        [XmlElement("Group")]
        string GroupName { get; set; }
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        [XmlElement("Settings")]
        List<Setting> Settings { get; set; }
    }
}
