// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="ISettingReader.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

/// <summary>
/// The Settings namespace.
/// </summary>
namespace Cube.XRM.Framework.Core.Settings
{
    /// <summary>
    /// Interface ISettingReader
    /// </summary>
    public interface ISettingReader
    {
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>List&lt;SettingGroup&gt;.</returns>
        List<SettingGroup> Read();
        /// <summary>
        /// Reads the specified setting file extension.
        /// </summary>
        /// <param name="SettingFileExtension">The setting file extension.</param>
        /// <returns>List&lt;SettingGroup&gt;.</returns>
        List<SettingGroup> Read(string SettingFileExtension);
    }
}
