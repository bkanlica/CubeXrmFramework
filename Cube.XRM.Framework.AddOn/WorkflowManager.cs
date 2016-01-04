// ***********************************************************************
// Assembly         : Cube.XRM.Framework.AddOn
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-18-2015
// ***********************************************************************
// <copyright file="WorkflowManager.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using System.Globalization;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;

/// <summary>
/// The AddOn namespace.
/// </summary>
namespace Cube.XRM.Framework.AddOn
{
    /// <summary>
    /// Class WorkflowManager.
    /// </summary>
    public abstract class WorkflowManager : CodeActivity
    {
        /// <summary>
        /// Gets or sets the throw exception.
        /// </summary>
        /// <value>The throw exception.</value>
        [Input("Throw Exception"), Default("true")]
        public InArgument<bool> ThrowException { get; set; }

        /// <summary>
        /// Gets or sets the is exception occured.
        /// </summary>
        /// <value>The is exception occured.</value>
        [Output("Is Exception Occured"), Default("false")]
        public OutArgument<bool> IsExceptionOccured { get; set; }

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        /// <value>The exception message.</value>
        [Output("Exception Message")]
        public OutArgument<string> ExceptionMessage { get; set; }

        /// <summary>
        /// Executes the specified cube base.
        /// </summary>
        /// <param name="cubeBase">The cube base.</param>
        protected abstract void Execute(CubeBase cubeBase);

        /// <summary>
        /// Executes the specified execution context.
        /// </summary>
        /// <param name="executionContext">The execution context.</param>
        /// <exception cref="System.ArgumentNullException">ExecutionContext is null</exception>
        /// <exception cref="Microsoft.Xrm.Sdk.InvalidPluginExecutionException">
        /// </exception>
        protected override void Execute(CodeActivityContext executionContext)
        {
            CubeBase cubeBase = new CubeBase();
            try
            {
                if (executionContext == null)
                {
                    throw new ArgumentNullException("ExecutionContext is null");
                }

                // Obtain the tracing service from the service provider.
                cubeBase.LogSystem = new DetailedLog() { TraceService = executionContext.GetExtension<ITracingService>() };

                cubeBase.LogSystem.CreateLog(string.Format(CultureInfo.InvariantCulture, "Entered the Execute() method : {0}", this.GetType().ToString()));

                // Obtain the execution context from the service provider.
                cubeBase.Context = executionContext.GetExtension<IWorkflowContext>();

                // Use the factory to generate the Organization Service.
                IOrganizationServiceFactory ServiceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
                cubeBase.XrmService = ServiceFactory.CreateOrganizationService(((IWorkflowContext)cubeBase.Context).UserId);

                cubeBase.BaseSystemObject = executionContext;

                Execute(cubeBase);
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
            {
                cubeBase.LogSystem.CreateLog("The application terminated with an Organization Service Fault error.");
                cubeBase.LogSystem.CreateLog(string.Format("Timestamp: {0}", ex.Detail.Timestamp));
                cubeBase.LogSystem.CreateLog(string.Format("Code: {0}", ex.Detail.ErrorCode));
                cubeBase.LogSystem.CreateLog(string.Format("Message: {0}", ex.Detail.Message));
                cubeBase.LogSystem.CreateLog(string.Format("Inner Fault: {0}",
                    null == ex.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault"));

                throw new InvalidPluginExecutionException(ex.Message, ex);
            }
            catch (System.TimeoutException ex)
            {
                cubeBase.LogSystem.CreateLog("The application terminated with an timeout error.");
                cubeBase.LogSystem.CreateLog(string.Format("Message: {0}", ex.Message));
                cubeBase.LogSystem.CreateLog(string.Format("Stack Trace: {0}", ex.StackTrace));
                cubeBase.LogSystem.CreateLog(string.Format("Inner Fault: {0}",
                    null == ex.InnerException.Message ? "No Inner Fault" : ex.InnerException.Message));

                throw new InvalidPluginExecutionException(ex.Message, ex);
            }
            catch (System.Exception ex)
            {
                cubeBase.LogSystem.CreateLog(string.Format(CultureInfo.InvariantCulture, "General Exception with message: {0}", ex.Message));
                if (ex.InnerException != null)
                {
                    cubeBase.LogSystem.CreateLog("Inner Exception Message:" + ex.InnerException.Message);

                    FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> fe = ex.InnerException
                        as FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>;
                    if (fe != null)
                    {
                        cubeBase.LogSystem.CreateLog(string.Format("Fault Exception Timestamp: {0}", fe.Detail.Timestamp));
                        cubeBase.LogSystem.CreateLog(string.Format("Fault Exception Code: {0}", fe.Detail.ErrorCode));
                        cubeBase.LogSystem.CreateLog(string.Format("Fault Exception Message: {0}", fe.Detail.Message));
                        cubeBase.LogSystem.CreateLog(string.Format("Fault Exception Trace: {0}", fe.Detail.TraceText));
                        cubeBase.LogSystem.CreateLog(string.Format("Inner Fault: {0}", null == fe.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault"));
                    }
                }

                throw new InvalidPluginExecutionException(ex.Message, ex);
            }
            finally
            {
                cubeBase.LogSystem.CreateLog(string.Format(CultureInfo.InvariantCulture, "Finished the Execute() method : {0}", this.GetType().ToString()));
            }
        }
    }
}

