// ***********************************************************************
// Assembly         : Cube.XRM.Framework.AddOn
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
using Cube.XRM.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xrm.Sdk;

/// <summary>
/// The AddOn namespace.
/// </summary>
namespace Cube.XRM.Framework.AddOn
{
    /// <summary>
    /// Class DetailedLog.
    /// </summary>
    public class DetailedLog : IDetailedLog
    {
        /// <summary>
        /// Gets or sets the trace service.
        /// </summary>
        /// <value>The trace service.</value>
        public ITracingService TraceService { get; set; }

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
        /// Creates the log.
        /// </summary>
        /// <param name="_context">The _context.</param>
        public void CreateLog(string _context)
        {
            if (string.IsNullOrWhiteSpace(_context) || this.TraceService == null)
            {
                return;
            }
            else
            {
                TraceService.Trace(_context);
            }
        }

        /// <summary>
        /// Creates the log.
        /// </summary>
        /// <param name="_context">The _context.</param>
        /// <param name="_level">The _level.</param>
        public void CreateLog(string _context, EventLogEntryType _level)
        {
            CreateLog(_context);
        }

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
    }
}
