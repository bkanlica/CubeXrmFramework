// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-18-2015
// ***********************************************************************
// <copyright file="CubeBase.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Cube.XRM.Framework.Core;
using Cube.XRM.Framework.Interfaces;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

/// <summary>
/// The Framework namespace.
/// </summary>
namespace Cube.XRM.Framework
{
    /// <summary>
    /// Class CubeBase.
    /// </summary>
    public class CubeBase
    {
        /// <summary>
        /// Gets or sets the plugin context.
        /// </summary>
        /// <value>The plugin context.</value>
        private IPluginExecutionContext PluginContext { get; set; }
        /// <summary>
        /// Gets or sets the workflow context.
        /// </summary>
        /// <value>The workflow context.</value>
        private IWorkflowContext WorkflowContext { get; set; }
        /// <summary>
        /// Gets or sets the plugin service provider.
        /// </summary>
        /// <value>The plugin service provider.</value>
        private IServiceProvider PluginServiceProvider { get; set; }
        /// <summary>
        /// Gets or sets the workflow code activity.
        /// </summary>
        /// <value>The workflow code activity.</value>
        private CodeActivityContext WorkflowCodeActivity { get; set; }

        /// <summary>
        /// Gets or sets the log system.
        /// </summary>
        /// <value>The log system.</value>
        public IDetailedLog LogSystem { get; set; }

        /// <summary>
        /// Gets or sets the XRM service.
        /// </summary>
        /// <value>The XRM service.</value>
        public IOrganizationService XrmService { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public object Context
        {
            get
            {
                if (PluginContext != null)
                    return PluginContext;
                else if (WorkflowContext != null)
                    return WorkflowContext;
                else
                    return null;
            }
            set
            {
                if (value is IPluginExecutionContext)
                    PluginContext = (IPluginExecutionContext)value;
                else if (value is IWorkflowContext)
                    WorkflowContext = (IWorkflowContext)value;
            }
        }

        /// <summary>
        /// Gets or sets the base system object.
        /// </summary>
        /// <value>The base system object.</value>
        public object BaseSystemObject
        {
            get
            {
                if (PluginServiceProvider != null)
                    return PluginServiceProvider;
                else if (WorkflowCodeActivity != null)
                    return WorkflowCodeActivity;
                else
                    return null;
            }
            set
            {
                if (value is IServiceProvider)
                    PluginServiceProvider = (IServiceProvider)value;
                else if (value is CodeActivityContext)
                    WorkflowCodeActivity = (CodeActivityContext)value;
            }
        }

        /// <summary>
        /// Gets the cud actions.
        /// </summary>
        /// <value>The cud actions.</value>
        public Actions XRMActions { get { return new Actions(LogSystem, XrmService); } }

        /// <summary>
        /// Gets the retrieve actions.
        /// </summary>
        /// <value>The retrieve actions.</value>
        public QueryActions RetrieveActions { get { return new QueryActions(LogSystem, XrmService); } }

        /// <summary>
        /// Gets the metadata retrieve actions.
        /// </summary>
        /// <value>The metadata retrieve actions.</value>
        public MetadataActions MetadataRetrieveActions { get { return new MetadataActions(LogSystem, XrmService); } }
    }
}
