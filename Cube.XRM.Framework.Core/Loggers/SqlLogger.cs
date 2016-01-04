// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-22-2015
// ***********************************************************************
// <copyright file="TextLogger.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Data;
using System.Data.SqlClient;
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
    class SqlLogger : ILogger
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
        /// Gets or sets the SQL connection.
        /// </summary>
        /// <value>The SQL connection.</value>
        public SqlConnection sqlConnection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLogger" /> class.
        /// </summary>
        public SqlLogger()
        {

        }

        /// <summary>
        /// Saves the specified _content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        /// <param name="_logType">Type of the _log.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>Result.</returns>
        public Result Save(string _content, EventLogEntryType _logType, object param)
        {
            content = _content;
            logType = _logType;
            sqlConnection = (SqlConnection)param;
            return AddToSQLTable();
        }


        /// <summary>
        /// Adds to SQL table.
        /// </summary>
        /// <returns>Result.</returns>
        private Result AddToSQLTable()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                string strSqlCommand = @"INSERT INTO [dbo].[CubeXRMLog]
                                           ([Date]
                                           ,[Type]
                                           ,[Message])
                                     VALUES
                                           ('{0}','{1}','{2}')";

                strSqlCommand = string.Format(strSqlCommand, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), logType.ToString(), content.Replace("'","\""));

                SqlCommand cmdDatabase = new SqlCommand(strSqlCommand);
                cmdDatabase.Connection = sqlConnection;
                cmdDatabase.ExecuteNonQuery();
                return new Result(false, "", null, null);
            }
            catch (Exception ex)
            {
                return new Result(true, ex.Message, null, null);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }
    }
}
