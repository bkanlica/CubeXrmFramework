// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="IDetailedLog.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Cube.XRM.Framework.Interfaces
{
    /// <summary>
    /// Interface IDetailedLog
    /// </summary>
    public interface IDetailedLog
    {
        /// <summary>
        /// Creates the log.
        /// </summary>
        /// <param name="_context">The _context.</param>
        void CreateLog(string _context);
        /// <summary>
        /// Creates the log.
        /// </summary>
        /// <param name="_context">The _context.</param>
        /// <param name="_level">The _level.</param>
        void CreateLog(string _context, EventLogEntryType _level);
        /// <summary>
        /// Logs the entity objects.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>System.String.</returns>
        string LogEntityObjects(Entity entity);

    }
}
