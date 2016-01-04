// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="QueryActions.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Cube.XRM.Framework.Interfaces;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.Reflection;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class QueryActions.
    /// </summary>
    public class QueryActions
    {
        /// <summary>
        /// Gets or sets the log system.
        /// </summary>
        /// <value>The log system.</value>
        private IDetailedLog logSystem { get; set; }

        /// <summary>
        /// Gets or sets the XRM service.
        /// </summary>
        /// <value>The XRM service.</value>
        private IOrganizationService xrmService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryActions" /> class.
        /// </summary>
        /// <param name="_logSystem">The _log system.</param>
        /// <param name="_xrmService">The _XRM service.</param>
        public QueryActions(IDetailedLog _logSystem, IOrganizationService _xrmService)
        {
            this.logSystem = _logSystem;
            this.xrmService = _xrmService;
        }

        /// <summary>
        /// Gets the item retrieve.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="EntityName">Name of the entity.</param>
        /// <param name="Columns">The columns.</param>
        /// <param name="AllColumns">if set to <c>true</c> [all columns].</param>
        /// <returns>Result.</returns>
        public Result getItemRetrieve(Guid Id, string EntityName, string[] Columns, bool AllColumns)
        {
            logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

            Entity ent = null;
            try
            {
                if (AllColumns)
                    ent = xrmService.Retrieve(EntityName, Id, new ColumnSet() { AllColumns = true });
                else if (Columns.Count() > 1)
                    ent = xrmService.Retrieve(EntityName, Id, new ColumnSet(Columns));
               
                if (ent != null)
                {
                    return new Result(false, string.Empty, ent, logSystem);
                }
                else
                    return new Result(false, "There is not any result.", null, logSystem);
            }
            catch (Exception ex)
            {
                return new Result(true,
                    MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                    null, logSystem);
            }
        }

        /// <summary>
        /// Gets the item fetch.
        /// </summary>
        /// <param name="FetchXml">The fetch XML.</param>
        /// <returns>Result.</returns>
        public Result getItemFetch(string FetchXml)
        {
            try
            {
                logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                DataCollection<Entity> entities = xrmService.RetrieveMultiple(new FetchExpression(FetchXml)).Entities;
                if (entities != null && entities.Count > 0)
                {
                    return new Result(false, string.Empty, entities[0], logSystem);
                }
                else
                    return new Result(false, "There is not any result.", null, logSystem);
            }
            catch (Exception ex)
            {
                return new Result(true,
                    MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                    null, logSystem);
            }
        }

        /// <summary>
        /// Gets the items fetch.
        /// </summary>
        /// <param name="FetchXml">The fetch XML.</param>
        /// <returns>Result.</returns>
        public Result getItemsFetch(string FetchXml)
        {
            try
            {
                logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                DataCollection<Entity> list = xrmService.RetrieveMultiple(new FetchExpression(FetchXml)).Entities;
                if (list != null && list.Count > 0)
                {
                    return new Result(false, string.Empty, list, logSystem);
                }
                else
                    return new Result(false, "There is not any result.", null, logSystem);
            }
            catch (Exception ex)
            {
                return new Result(true,
                   MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                   null, logSystem);
            }
        }

        /// <summary>
        /// Gets as lookup.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Result.</returns>
        public Result GetAsLookup(string entityName, string attributeName)
        {
            try
            {
                string fetchXML = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' no-lock='true'>
                                      <entity name='{0}'>
                                        <attribute name='{1}' />
                                        <order attribute='{1}' descending='true' />
                                      </entity>
                                    </fetch>";

                fetchXML = string.Format(fetchXML, entityName, attributeName);

                DataCollection<Entity> entities = xrmService.RetrieveMultiple(new FetchExpression(fetchXML)).Entities;
                if (entities != null && entities.Count > 0)
                {
                    foreach (Entity item in entities)
                    {
                        ObjectCarrier.SetValue(item.Id.ToString(), item[attributeName].ToString());
                    }
                }

                return new Result(false, string.Empty, ObjectCarrier.GetAllValues(), logSystem);
            }
            catch (Exception ex)
            {
                return new Result(true,
                   MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                   null, logSystem);
            }
        }
    }
}
