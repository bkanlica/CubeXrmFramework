// ***********************************************************************
// Assembly         : Cube.XRM.Framework.IntegrationTester
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-21-2015
// ***********************************************************************
// <copyright file="WorkflowSample.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cube.XRM.Framework.AddOn;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The IntegrationTester namespace.
/// </summary>
namespace Cube.XRM.Framework.IntegrationTester
{
    /// <summary>
    /// Class WorkflowSample.
    /// </summary>
    class WorkflowSample : WorkflowManager
    {
        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        [Input("Lookup Field Name")]
        public InArgument<string> parameter { get; set; }

        /// <summary>
        /// Executes the workflow activity.
        /// </summary>
        /// <param name="cube">The cube.</param>
        protected override void Execute(CubeBase cube)
        {
            Result r = cube.XRMActions.Execute<CreateRequest, CreateResponse>(new CreateRequest() {  });
        }
    }
}
