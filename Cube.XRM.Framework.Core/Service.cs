// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-21-2015
// ***********************************************************************
// <copyright file="Service.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Cube.XRM.Framework.Interfaces;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Reflection;
using System.ServiceModel.Description;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class Service.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Gets or sets the log system.
        /// </summary>
        /// <value>The log system.</value>
        private IDetailedLog logSystem { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service" /> class.
        /// </summary>
        /// <param name="_logger">The _logger.</param>
        public Service(IDetailedLog _logger)
        {
            logSystem = _logger;
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <returns>IOrganizationService.</returns>
        public Result GetService(string URL, string UserName, string Password)
        {
            try
            {
                if (logSystem != null)
                    logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                Uri organizationUri = new Uri(URL);
                ClientCredentials clientcred = new ClientCredentials();
                clientcred.UserName.UserName = UserName;
                clientcred.UserName.Password = Password;
                OrganizationServiceProxy _serviceproxy = new OrganizationServiceProxy(organizationUri, null, clientcred, null);
                IOrganizationService _service = (IOrganizationService)_serviceproxy;

                return new Result(false, string.Empty, _service, logSystem);
            }
            catch (Exception ex)
            {
                return new Result(true,
                    MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                    null, logSystem);
            }
        }

        /// <summary>
        /// Takes service information and create a OrganizationService object
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>ready use a CRM Service</returns>
        public Result GetService(string connectionString)
        {
            try
            {
                if (logSystem != null)
                    logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                if (isValidConnectionString(connectionString))
                {
                    // Establish a connection to the organization web service using CrmConnection.
                    CrmConnection connection = CrmConnection.Parse(connectionString);
                    OrganizationService proxy = new OrganizationService(connection);

                    return new Result(false, string.Empty, proxy, logSystem);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return new Result(true,
                    MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                    null, logSystem);
            }
        }

        /// <summary>
        /// Verifies if a connection string is valid for Microsoft Dynamics CRM.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>True for a valid string, otherwise False.</returns>
        private Boolean isValidConnectionString(String connectionString)
        {
            // At a minimum, a connection string must contain one of these arguments.
            if (connectionString.Contains("Url=") ||
                connectionString.Contains("Server=") ||
                connectionString.Contains("ServiceUri="))
                return true;

            return false;
        }
    }
}