// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-21-2015
// ***********************************************************************
// <copyright file="EnumCarrier.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class EnumCarrier.
    /// </summary>
    public class EnumCarrier
    {
        /// <summary>
        /// Enum LogLevel
        /// </summary>
        public enum LogLevel
        {
            /// <summary>
            /// The none
            /// </summary>
            None = -1,
            /// <summary>
            /// All
            /// </summary>
            All = 0,
            /// <summary>
            /// The error
            /// </summary>
            Error = 1,
            /// <summary>
            /// The information
            /// </summary>
            Information = 2,
            /// <summary>
            /// The warning
            /// </summary>
            Warning = 3,
            /// <summary>
            /// The success audit
            /// </summary>
            SuccessAudit = 4,
            /// <summary>
            /// The failure audit
            /// </summary>
            FailureAudit = 5
        }

        /// <summary>
        /// Enum LogLocation
        /// </summary>
        public enum LogLocation
        {
            /// <summary>
            /// The none
            /// </summary>
            None = -1,
            /// <summary>
            /// The CRM
            /// </summary>
            CRM = 0,
            /// <summary>
            /// The SQL
            /// </summary>
            SQL = 1,
            /// <summary>
            /// The text
            /// </summary>
            Text = 2
        }

        /// <summary>
        /// Enum LogScreen
        /// </summary>
        public enum LogScreen
        {
            /// <summary>
            /// The none
            /// </summary>
            None = -1,
            /// <summary>
            /// All
            /// </summary>
            All = 0,
            /// <summary>
            /// The diagnostic
            /// </summary>
            Diagnostic = 1,
            /// <summary>
            /// The console
            /// </summary>
            Console = 2
        }
    }
}
