// ***********************************************************************
// Assembly         : Cube.XRM.Framework
// Author           : metro.BKanlica
// Created          : 08-12-2015
//
// Last Modified By : metro.BKanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="FrameworkInitializer.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cube.XRM.Framework.Core.Loggers;
using Cube.XRM.Framework.Core.Settings;
using Cube.XRM.Framework.Interfaces;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

/// <summary>
/// The Framework namespace.
/// </summary>
namespace Cube.XRM.Framework.Core
{
    /// <summary>
    /// Class FrameworkInitializer.
    /// </summary>
    public class FrameworkInitializer
    {
        public CubeBase cube { get; set; }
        /// <summary>
        /// Initializers the specified system setting reader.
        /// </summary>
        /// <param name="SystemSettingReader">The system setting reader.</param>
        private void Initializer(ISettingReader SystemSettingReader)
        {
            ILogger SystemLogger = null;
            IOrganizationService XrmService = null;
            CubeBase SystemBase = null;

            List<SettingGroup> SystemSettingsGroup = SystemSettingReader.Read("XRM");
            //load global settings
            if (SystemSettingsGroup != null && SystemSettingsGroup.Count > 0)
            {
                SettingGroup groupGlobalSettings = SystemSettingsGroup.FirstOrDefault(s => s.GroupName == "GlobalSettings");
                EnumCarrier.LogLevel logLevel = EnumCarrier.LogLevel.None;
                EnumCarrier.LogScreen logScreen = EnumCarrier.LogScreen.None;
                EnumCarrier.LogLocation logger = EnumCarrier.LogLocation.None;

                if (groupGlobalSettings != null && groupGlobalSettings.Settings != null && groupGlobalSettings.Settings.Count > 0)
                {
                    //load logger settings                    
                    logLevel = (EnumCarrier.LogLevel)groupGlobalSettings.Settings.FirstOrDefault(l => l.Key == "LogLevel").Value;
                    logScreen = (EnumCarrier.LogScreen)groupGlobalSettings.Settings.FirstOrDefault(l => l.Key == "LogScreen").Value;
                    logger = (EnumCarrier.LogLocation)groupGlobalSettings.Settings.FirstOrDefault(l => l.Key == "LogLocation").Value;
                    switch (logger)
                    {
                        case EnumCarrier.LogLocation.CRM:
                            break;
                        case EnumCarrier.LogLocation.SQL:
                            SystemLogger = new SqlLogger();
                            break;
                        case EnumCarrier.LogLocation.Text:
                            SystemLogger = new TextLogger();
                            break;
                    }
                }

                SettingGroup groupConnections = SystemSettingsGroup.FirstOrDefault(s => s.GroupName == "Connections");
                if (groupConnections != null && groupConnections.Settings != null && groupConnections.Settings.Count > 0)
                {
                    IDetailedLog logSystem = null;
                    if (logger == EnumCarrier.LogLocation.SQL)
                    {
                        string connectionString = groupConnections.Settings.FirstOrDefault(l => l.Key == "SQL").Value.ToString();
                        SqlConnection connection = new SqlConnection(connectionString);
                        logSystem = new DetailedLog(SystemLogger, logLevel, connection, logScreen);
                    }
                    else if (logger == EnumCarrier.LogLocation.Text)
                    {
                        string logPath = groupGlobalSettings.Settings.FirstOrDefault(l => l.Key == "LogPath").Value.ToString();
                        logSystem = new DetailedLog(SystemLogger, logLevel, logPath, logScreen);
                    }

                    SystemBase = new CubeBase();
                    SystemBase.LogSystem = logSystem;

                    bool OpenOrganizationService = (bool)groupGlobalSettings.Settings.FirstOrDefault(l => l.Key == "OpenOrganizationService").Value;
                    if (OpenOrganizationService)
                    {
                        string crmConnection = groupConnections.Settings.FirstOrDefault(l => l.Key == "CRM").Value.ToString();
                        Service service = new Service(SystemBase.LogSystem);
                        Result result = service.GetService(crmConnection);
                        if (!result.isError)
                        {
                            XrmService = (IOrganizationService)result.BusinessObject;
                            SystemBase.XrmService = XrmService;
                        }
                    }

                   
                }

                cube = SystemBase;
                cube.XRMActions.Create(new Entity());
                cube.RetrieveActions.getItemsFetch("");
                cube.MetadataRetrieveActions.GetOptionSets("","");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInitializer" /> class.
        /// </summary>
        public FrameworkInitializer()
        {
            //load first settings group
            //this is most important point of framework
            //we are saying to sytem settings location
            //if this different than xml you have to change it.
            //if you are using CRM online you must change it to CRMSettingReader
            ISettingReader SystemSettingReader = new XMLSettingReader();
            Initializer(SystemSettingReader);
        }
    }
}
