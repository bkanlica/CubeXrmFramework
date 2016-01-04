// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-21-2015
// ***********************************************************************
// <copyright file="ILogger.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Xrm.Sdk;
using System.Data.SqlClient;
using System.Diagnostics;

/// <summary>
/// The Loggers namespace.
/// </summary>
namespace Cube.XRM.Framework.Core.Loggers
{
    /// <summary>
    /// Interface ILogger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Saves the specified _content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        /// <param name="_logType">Type of the _log.</param>
        /// <param name="_logPath">The _log path.</param>
        /// <returns>System.String.</returns>
        Result Save(string _content, EventLogEntryType _logType, object param);
    }
}
