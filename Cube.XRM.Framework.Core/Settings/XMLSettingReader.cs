// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-21-2015
// ***********************************************************************
// <copyright file="XMLSettingReader.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

/// <summary>
/// The Settings namespace.
/// </summary>
namespace Cube.XRM.Framework.Core.Settings
{
    /// <summary>
    /// Class XMLSettingReader.
    /// </summary>
    public class XMLSettingReader : ISettingReader
    {
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>List&lt;SettingGroup&gt;.</returns>
        public List<SettingGroup> Read()
        {
            string SettingFileExtension = ConfigurationManager.AppSettings["SettingFileExtension"];
            return Read(SettingFileExtension);
        }

        /// <summary>
        /// Reads the specified setting file extension.
        /// </summary>
        /// <param name="SettingFileExtension">The setting file extension.</param>
        /// <returns>List&lt;SettingGroup&gt;.</returns>
        public List<SettingGroup> Read(string SettingFileExtension)
        {
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<SettingGroup>), null, null, new XmlRootAttribute("SettingGroups"), "Cube.XRM.Framework");
                TextReader reader = new StreamReader(String.Format(@"C:\Cube\Settings\SettingsFile - {0}.xml", SettingFileExtension));
                object obj = deserializer.Deserialize(reader);
                List<SettingGroup> XmlData = (List<SettingGroup>)obj;
                reader.Close();
                return XmlData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
