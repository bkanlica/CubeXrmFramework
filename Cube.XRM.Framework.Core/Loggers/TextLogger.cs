// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="TextLogger.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.IO;

/// <summary>
/// The Loggers namespace.
/// </summary>
namespace Cube.XRM.Framework.Core.Loggers
{
    /// <summary>
    /// Class TextLogger.
    /// </summary>
    class TextLogger : ILogger
    {

        /// <summary>
        /// The content
        /// </summary>
        private string content = "";

        /// <summary>
        /// The log type
        /// </summary>
        private EventLogEntryType logType = EventLogEntryType.Information;

        /// <summary>
        /// Gets or sets the log path.
        /// </summary>
        /// <value>The log path.</value>
        private string logPath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLogger" /> class.
        /// </summary>
        public TextLogger()
        {

        }

        /// <summary>
        /// Saves the specified _content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        /// <param name="_logType">Type of the _log.</param>
        /// <returns>System.String.</returns>
        public Result Save(string _content, EventLogEntryType _logType)
        {
            content = _content;
            logType = _logType;
            logPath = @"C:\Cube\Logs\";
            return AddToFile();
        }

        /// <summary>
        /// Saves the specified _content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        /// <param name="_logType">Type of the _log.</param>
        /// <param name="_logPath">The _log path.</param>
        /// <returns>System.String.</returns>
        public Result Save(string _content, EventLogEntryType _logType, object param)
        {
            content = _content;
            logType = _logType;
            logPath = param.ToString();
            return AddToFile();
        }


        /// <summary>
        /// Adds to file.
        /// </summary>
        /// <returns>System.String.</returns>
        private Result AddToFile()
        {
            try
            {
                logPath += @"\Log_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + ".txt";

                FileStream fs = new FileStream(logPath,
                    FileMode.OpenOrCreate, FileAccess.Write);

                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
                return new Result(false, "", null, null);
            }
            catch (Exception ex)
            {
                return new Result(true, ex.Message, null, null);
            }
        }
    }
}
