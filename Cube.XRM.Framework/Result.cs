// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="Result.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Cube.XRM.Framework.Interfaces;
using System;
using System.Runtime.Serialization;

/// <summary>
/// The Framework namespace.
/// </summary>
namespace Cube.XRM.Framework
{
    /// <summary>
    /// Result is a class for send the result of usage of a method
    /// </summary>
    [DataContract]
    public class Result
    {
        /// <summary>
        /// Gets or sets the log system.
        /// </summary>
        /// <value>The log system.</value>
        private IDetailedLog LogSystem { get; set; }
        /// <summary>
        /// if there is an error result of a method you can see the detail of error in this property
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        public string Message { get; set; }
        /// <summary>
        /// if there is an error it will be return true otherwise return false
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool isError { get; set; }
        /// <summary>
        /// if method send an object as a result, it will be in this object file
        /// </summary>
        /// <value>The business object.</value>
        [DataMember]
        public Object BusinessObject { get; set; }
        /// <summary>
        /// if method send an array as a result, it will be in this object array file
        /// </summary>
        /// <value>The business object array.</value>
        [DataMember]
        public Object[] BusinessObjectArray { get; set; }
        /// <summary>
        /// Default constractor of Result Class for Object
        /// </summary>
        /// <param name="_isError">if method send an object as a result, it will be in this object file</param>
        /// <param name="_Message">if there is an error result of a method you can see the detail of error in this property</param>
        /// <param name="_BusinessObject">if method send an object as a result, it will be in this object file</param>
        /// <param name="_LogSystem">The _ log system.</param>
        public Result(bool _isError, string _Message, Object _BusinessObject, IDetailedLog _LogSystem)
        {
            Message = _Message;
            isError = _isError;
            BusinessObject = _BusinessObject;
            LogSystem = _LogSystem;

            if (Message != null && Message != string.Empty)
            {
                System.Diagnostics.EventLogEntryType logType = System.Diagnostics.EventLogEntryType.Information;
                if (isError)
                    logType = System.Diagnostics.EventLogEntryType.Error;

                if (LogSystem != null)
                    LogSystem.CreateLog(Message, logType);
            }
        }
        /// <summary>
        /// Default constractor of Result Class for Array
        /// </summary>
        /// <param name="_isError">if method send an object as a result, it will be in this object file</param>
        /// <param name="_Message">if there is an error result of a method you can see the detail of error in this property</param>
        /// <param name="_BusinessObjectArray">if method send an array as a result, it will be in this object array file</param>
        /// <param name="_LogSystem">The _ log system.</param>
        public Result(bool _isError, string _Message, Object[] _BusinessObjectArray, IDetailedLog _LogSystem)
        {
            Message = _Message;
            isError = _isError;
            BusinessObjectArray = _BusinessObjectArray;
            LogSystem = _LogSystem;

            if (Message != null && Message != string.Empty)
            {
                System.Diagnostics.EventLogEntryType logType = System.Diagnostics.EventLogEntryType.Information;
                if (isError)
                    logType = System.Diagnostics.EventLogEntryType.Error;

                if (LogSystem != null)
                    LogSystem.CreateLog(Message, logType);
            }
        }
    }
}
