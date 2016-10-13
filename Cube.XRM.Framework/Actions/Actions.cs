// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-21-2015
// ***********************************************************************
// <copyright file="Actions.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Cube.XRM.Framework.Interfaces;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Reflection;

/// <summary>
/// The Core namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class Actions.
    /// </summary>
    public class Actions
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
        /// Gets or sets a value indicating whether [log entity items].
        /// </summary>
        /// <value><c>true</c> if [log entity items]; otherwise, <c>false</c>.</value>
        private bool logEntityItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actions" /> class.
        /// </summary>
        /// <param name="_logSystem">The _log system.</param>
        /// <param name="_xrmService">The _XRM service.</param>
        public Actions(IDetailedLog _logSystem, IOrganizationService _xrmService)
        {
            this.logSystem = _logSystem;
            this.xrmService = _xrmService;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Actions"/> class.
        /// </summary>
        /// <param name="_logSystem">The _log system.</param>
        /// <param name="_xrmService">The _XRM service.</param>
        /// <param name="_logEntityItems">if set to <c>true</c> [_log entity items].</param>
        public Actions(IDetailedLog _logSystem, IOrganizationService _xrmService, bool _logEntityItems)
        {
            this.logSystem = _logSystem;
            this.xrmService = _xrmService;
            this.logEntityItems = _logEntityItems;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Result.</returns>
        /// <exception cref="System.Exception">Entity or service is null</exception>
        /// <exception cref="Exception">Entity or service is null</exception>
        public virtual Result Create(Entity entity)
        {
            try
            {
                logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                if (entity != null && xrmService != null)
                {
                    Guid id = xrmService.Create(entity);
                    logSystem.CreateLog("Entity Created! Type: " + entity.LogicalName + ", ID : " + entity.Id, System.Diagnostics.EventLogEntryType.Information);

                    if (logEntityItems)
                        logSystem.LogEntityObjects(entity);

                    return new Result(false, string.Empty, id, logSystem);
                }
                else
                    throw new Exception("Entity or service is null");
            }
            catch (Exception ex)
            {
                return new Result(true,
                    MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                    null, logSystem);
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Result.</returns>
        /// <exception cref="System.Exception">Entity or service is null</exception>
        /// <exception cref="Exception">Entity or service is null</exception>
        public virtual Result Update(Entity entity)
        {

            try
            {
                logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                if (entity != null && entity.Id != Guid.Empty && xrmService != null)
                {
                    xrmService.Update(entity);
                    logSystem.CreateLog("Entity Update! Type: " + entity.LogicalName + ", ID : " + entity.Id, System.Diagnostics.EventLogEntryType.Information);

                    if (logEntityItems)
                        logSystem.LogEntityObjects(entity);

                    return new Result(false, string.Empty, entity.Id, logSystem);
                }
                else
                    throw new Exception("Entity or service is null");
            }
            catch (Exception ex)
            {
                return new Result(true,
                     MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                     null, logSystem);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="Entity">The entity.</param>
        /// <returns>Result.</returns>
        /// <exception cref="System.Exception">Entity information or service is null</exception>
        /// <exception cref="Exception">Entity information or service is null</exception>
        public virtual Result Delete(Guid ID, string Entity)
        {
            try
            {
                logSystem.CreateLog(System.Reflection.MethodBase.GetCurrentMethod().ToString() + " is called");

                if (ID != Guid.Empty && Entity != null && Entity != string.Empty && xrmService != null)
                {
                    xrmService.Delete(Entity, ID);
                    logSystem.CreateLog("Entity Deleted! Type: " + Entity + ", ID : " + ID, System.Diagnostics.EventLogEntryType.Information);

                    return new Result(false, string.Empty, ID, logSystem);
                }
                else
                    throw new Exception("Entity information or service is null");
            }
            catch (Exception ex)
            {
                return new Result(true,
                     MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                     null, logSystem);
            }
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="status">The status.</param>
        /// <param name="entityId">The entity identifier.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>Result.</returns>
        public Result SetState(int state, int status, Guid entityId, string entityType)
        {
            try
            {
                logSystem.CreateLog("SetState Rrequest: Type: " + entityType + ", ID : " + entityId + ", State :" + state + ", Status :" + status
                    , System.Diagnostics.EventLogEntryType.Information);

                // Create the Request Object
                SetStateRequest stateRequest = new SetStateRequest();

                // Set the Request Object's Properties
                stateRequest.State = new OptionSetValue(state);
                stateRequest.Status = new OptionSetValue(status);

                // Point the Request to the case whose state is being changed
                stateRequest.EntityMoniker = new EntityReference(entityType, entityId);

                // Execute the Request
                SetStateResponse stateSet = (SetStateResponse)xrmService.Execute(stateRequest);

                
                return Execute<SetStateRequest, SetStateResponse>(stateRequest);        
            }
            catch (Exception ex)
            {
                return new Result(true,
                      MethodBase.GetCurrentMethod().ToString() + " : " + ExceptionHandler.HandleException(ex),
                      null, logSystem);
            }
        }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="request">The request.</param>
        /// <returns>Result.</returns>
        public Result Execute<T, V>(T request) where V : OrganizationResponse where T : OrganizationRequest
        {
            try
            {
                logSystem.CreateLog(typeof(T).ToString() + " Request executing...");

                V response = (V)xrmService.Execute(request);

                logSystem.CreateLog(typeof(T).ToString() + " Request successfully executed");
                return new Result(false, string.Empty, logSystem, response);
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
