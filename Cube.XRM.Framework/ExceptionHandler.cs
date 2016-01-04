// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="ExceptionHandler.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Xml;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class ExceptionHandler.
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>System.String.</returns>
        public static string HandleException(Exception ex)
        {
            string Error = "Error Message : " + ex.Message + System.Environment.NewLine;

            if (ex.InnerException != null)
                Error += ex.InnerException.Message + System.Environment.NewLine;

            System.Web.Services.Protocols.SoapException se = ex as System.Web.Services.Protocols.SoapException;
            if (se != null)
            {
                Error += "Code : " + GetError(se.Detail, "//code") + System.Environment.NewLine;
                Error += "Desription : " + GetError(se.Detail, "//description") + System.Environment.NewLine;
                Error += "Type : " + GetError(se.Detail, "//type") + System.Environment.NewLine;
            }
            return Error;
        }

        /// <summary>
        /// Returns the error code that is contained in SoapException.Detail.
        /// </summary>
        /// <param name="errorInfo">An XmlNode that contains application specific error information.</param>
        /// <param name="item">The item.</param>
        /// <returns>Error code or zero.</returns>
        private static string GetError(XmlNode errorInfo, string item)
        {
            XmlNode code = errorInfo.SelectSingleNode(item);

            if (code != null)
                return code.InnerText;
            else
                return "";
        }
    }
}
