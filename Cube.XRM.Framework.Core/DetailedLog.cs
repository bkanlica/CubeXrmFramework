// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="DetailedLog.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Cube.XRM.Framework.Core.Loggers;
using Cube.XRM.Framework.Interfaces;
using Microsoft.Xrm.Sdk;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class DetailedLog.
    /// </summary>
    public class DetailedLog : IDetailedLog
    {
        #region private attributes
        /// <summary>
        /// Gets or sets the log screen.
        /// </summary>
        /// <value>The log screen.</value>
        private EnumCarrier.LogScreen logScreen { get; set; }
        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        /// <value>The log level.</value>
        private EnumCarrier.LogLevel logLevel { get; set; }
        /// <summary>
        /// Gets or sets the log path.
        /// </summary>
        /// <value>The log path.</value>
        private string logPath { get; set; }
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger logger { get; set; }

        private SqlConnection loggerSqlConnection { get; set; }


        #endregion

        #region private methods
        /// <summary>
        /// Logs to screen.
        /// </summary>
        /// <param name="_context">The _context.</param>
        private void LogToScreen(string _context)
        {
            Console.WriteLine(_context);
        }

        /// <summary>
        /// Logs to diagnostic.
        /// </summary>
        /// <param name="_context">The _context.</param>
        private void LogToDiagnostic(string _context)
        {
            System.Diagnostics.Trace.TraceInformation(_context);

        }

        /// <summary>
        /// Shows the log.
        /// </summary>
        /// <param name="_context">The _context.</param>
        private void ShowLog(string _context)
        {

            if (logScreen != EnumCarrier.LogScreen.None)
            {
                switch (logScreen)
                {
                    case EnumCarrier.LogScreen.All:
                        LogToScreen(_context);
                        LogToDiagnostic(_context);
                        break;
                    case EnumCarrier.LogScreen.Diagnostic:
                        LogToDiagnostic(_context);
                        break;
                    case EnumCarrier.LogScreen.Console:
                        LogToScreen(_context);
                        break;
                }
            }
        }

        /// <summary>
        /// Formats the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private string FormatValue(object value)
        {
            if (value is EntityReference)
            {
                var data = value as EntityReference;
                return string.Format("{0}:{1}", data.LogicalName, data.Id);
            }
            if (value is OptionSetValue)
            {
                var data = value as OptionSetValue;
                return string.Format("{0}", data.Value);
            }

            if (value != null)
            {
                return value.ToString();
            }

            return String.Empty;
        }

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="_content">The _content.</param>
        /// <param name="_logType">Type of the _log.</param>
        private void WriteLog(string _content, EventLogEntryType _logType)
        {
            try
            {
                bool logIt = false;
                if (logLevel != EnumCarrier.LogLevel.All && _logType != EventLogEntryType.Error)
                {
                    if (_logType == EventLogEntryType.Information && (int)logLevel >= 2)
                        logIt = true;
                    if (_logType == EventLogEntryType.Warning && (int)logLevel >= 3)
                        logIt = true;
                    if (_logType == EventLogEntryType.SuccessAudit && (int)logLevel >= 4)
                        logIt = true;
                    if (_logType == EventLogEntryType.FailureAudit && (int)logLevel >= 5)
                        logIt = true;
                }
                else
                    logIt = true;

                if (logIt)
                {
                    if (logger is TextLogger)
                    {
                        string text = DateTime.Now.ToShortTimeString() + " : " + EventLogEntryType.Information.ToString() + " : " + _content;
                        logger.Save(text, _logType, logPath);
                    }
                    else if (logger is SqlLogger)
                        logger.Save(_content, _logType, loggerSqlConnection);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Creates the log.
        /// </summary>
        /// <param name="_content">The _context.</param>
        public void CreateLog(string _content)
        {
            ShowLog(_content);
            WriteLog(_content, EventLogEntryType.Information);
        }

        /// <summary>
        /// Creates the log.
        /// </summary>
        /// <param name="_context">The _context.</param>
        /// <param name="_level">The _level.</param>
        public void CreateLog(string _content, EventLogEntryType _level)
        {
            ShowLog(_content);
            WriteLog(_content, _level);
        }

        /// <summary>
        /// Logs the entity objects.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public string LogEntityObjects(Entity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\r\nEntity: {0}", entity.LogicalName);
            sb.AppendFormat("\r\nId: {0}", entity.Id);

            if (entity.Attributes != null)
            {
                sb.Append("\r\nAttributes Collection:");
                foreach (var item in entity.Attributes)
                {
                    sb.AppendFormat("\r\n\t{0} = {1}", item.Key, FormatValue(item.Value));
                }
            }

            CreateLog(sb.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedLog" /> class.
        /// </summary>
        /// <param name="_logger">The _logger.</param>
        /// <param name="_level">The _level.</param>
        /// <param name="_path">The _path.</param>
        /// <param name="_screen">The _screen.</param>
        public DetailedLog(ILogger _logger, EnumCarrier.LogLevel _level, string _path, EnumCarrier.LogScreen _screen)
        {
            this.logger = _logger;
            this.logLevel = _level;
            this.logPath = _path;
            this.logScreen = _screen;
        }

        public DetailedLog(ILogger _logger, EnumCarrier.LogLevel _level, SqlConnection _connection, EnumCarrier.LogScreen _screen)
        {
            this.logger = _logger;
            this.logLevel = _level;
            this.loggerSqlConnection = _connection;
            this.logScreen = _screen;
        }
        #endregion
    }
}
