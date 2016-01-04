// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="MetadataActions.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Cube.XRM.Framework.Interfaces;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class MetadataActions.
    /// </summary>
    public class MetadataActions
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
        /// Initializes a new instance of the <see cref="MetadataActions" /> class.
        /// </summary>
        /// <param name="_logSystem">The _log system.</param>
        /// <param name="_xrmService">The _XRM service.</param>
        public MetadataActions(IDetailedLog _logSystem, IOrganizationService _xrmService)
        {
            this.logSystem = _logSystem;
            this.xrmService = _xrmService;
        }

        /// <summary>
        /// Gets the option sets.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Result.</returns>
        public Result GetOptionSets(string entityName, string attributeName)
        {
            try
            {
                logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                string AttributeName = attributeName;
                string EntityLogicalName = entityName;
                RetrieveEntityRequest retrieveDetails = new RetrieveEntityRequest
                {
                    EntityFilters = EntityFilters.All,
                    LogicalName = EntityLogicalName
                };
                RetrieveEntityResponse retrieveEntityResponseObj = (RetrieveEntityResponse)xrmService.Execute(retrieveDetails);
                Microsoft.Xrm.Sdk.Metadata.EntityMetadata metadata = retrieveEntityResponseObj.EntityMetadata;
                Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata picklistMetadata = metadata.Attributes.FirstOrDefault(attribute => String.Equals
    (attribute.LogicalName, attributeName, StringComparison.OrdinalIgnoreCase)) as Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata;

                foreach (OptionMetadata item in picklistMetadata.OptionSet.Options)
                {
                    ObjectCarrier.SetValue(item.Value.ToString(), item.Label.UserLocalizedLabel.Label);
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

        /// <summary>
        /// Gets the option set text.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="optionSetValue">The option set value.</param>
        /// <returns>Result.</returns>
        public Result GetOptionSetText(string entityName, string attributeName, int? optionSetValue)
        {
            try
            {
                logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                string AttributeName = attributeName;
                string EntityLogicalName = entityName;
                RetrieveEntityRequest retrieveDetails = new RetrieveEntityRequest
                {
                    EntityFilters = EntityFilters.All,
                    LogicalName = EntityLogicalName
                };
                RetrieveEntityResponse retrieveEntityResponseObj = (RetrieveEntityResponse)xrmService.Execute(retrieveDetails);
                Microsoft.Xrm.Sdk.Metadata.EntityMetadata metadata = retrieveEntityResponseObj.EntityMetadata;
                Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata picklistMetadata = metadata.Attributes.FirstOrDefault(attribute => String.Equals
    (attribute.LogicalName, attributeName, StringComparison.OrdinalIgnoreCase)) as Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata;
                Microsoft.Xrm.Sdk.Metadata.OptionSetMetadata options = picklistMetadata.OptionSet;
                IList<OptionMetadata> OptionsList = (from o in options.Options
                                                     where o.Value.Value == optionSetValue
                                                     select o).ToList();
                string optionsetLabel = (OptionsList.First()).Label.UserLocalizedLabel.Label;
                return new Result(false, string.Empty, optionsetLabel, logSystem);
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
