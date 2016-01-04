// ***********************************************************************
// Assembly         : Cube.XRM.Framework.IntegrationTester
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-18-2015
// ***********************************************************************
// <copyright file="Program.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cube.XRM.Framework.Core;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

/// <summary>
/// The IntegrationTester namespace.
/// </summary>
namespace Cube.XRM.Framework.IntegrationTester
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            //CreateXMLFile();
            //DeSerialize();
            FrameworkInitializer f = new FrameworkInitializer();
            //WorkflowTester();
        }

        /// <summary>
        /// Creates the XML file.
        /// </summary>
        static void CreateXMLFile()
        {
            List<SettingGroup> settingGroups = new List<SettingGroup>();
            List<Setting> settings1 = new List<Setting>();
            List<Setting> settings2 = new List<Setting>();

            Setting s1 = new Setting();
            Setting s2 = new Setting();

            s1.Key = "CRM";
            s1.Value = "Url=https://xxxyyyzzz.crm4.dynamics.com; Username=333; Password=444;";
            s2.Key = "SQL";
            s2.Value = "Server=Your_Server_Name; Database=Your_Database_Name; Trusted_Connection=yes;";

            settings1.Add(s1);
            settings1.Add(s2);
            
            Setting s3 = new Setting();
            Setting s4 = new Setting();
            Setting s5 = new Setting();
            Setting s6 = new Setting();
            Setting s7 = new Setting();

            s3.Key = "LogLevel"; //<!--Error, Info, All-->
            s3.Value = EnumCarrier.LogLevel.All;
            s4.Key = "LogLocation"; //<!--CRM, SQL, Text-->
            s4.Value = EnumCarrier.LogLocation.Text;
            s5.Key = "LogScreen"; //<!--Diagnostic, Console, All-->
            s5.Value = EnumCarrier.LogScreen.All;
            s6.Key = "LogPath";
            s6.Value = @"C:\Cube\Logs";
            s7.Key = "OpenOrganizationService";
            s7.Value = true;

            settings2.Add(s3);
            settings2.Add(s4);
            settings2.Add(s5);
            settings2.Add(s6);
            settings2.Add(s7);

            SettingGroup group1 = new SettingGroup();
            group1.GroupName = "Connections";

            SettingGroup group2 = new SettingGroup();
            group2.GroupName = "GlobalSettings";

            group1.Settings = settings1;
            group2.Settings = settings2;

            settingGroups.Add(group1);
            settingGroups.Add(group2);

            Serialize(settingGroups);
        }

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        static void Serialize<T>(T value)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(T),null,null,new XmlRootAttribute("SettingGroups"),"Cube.XRM.Framework");

            var path = @"C:\Cube\Settings\SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, value);
            file.Close();

        }

        /// <summary>
        /// Des the serialize.
        /// </summary>
        /// <returns>List&lt;SettingGroup&gt;.</returns>
        static List<SettingGroup> DeSerialize()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<SettingGroup>), null, null, new XmlRootAttribute("SettingGroups"), "Cube.XRM.Framework");
            TextReader reader = new StreamReader(@"C:\Cube\Settings\SerializationOverview.xml");
            object obj = deserializer.Deserialize(reader);
            List<SettingGroup> XmlData = (List<SettingGroup>)obj;
            reader.Close();
            return XmlData;
        }

        /// <summary>
        /// Workflows the tester.
        /// </summary>
        static void WorkflowTester()
        {
            FrameworkInitializer fi = new FrameworkInitializer();
            try
            {               
                //DetailedLog.CreateLog("ContactCheckTester " + type.ToString());
                var workflowUserId = Guid.NewGuid();
                var workflowCorrelationId = Guid.NewGuid();
                var workflowInitiatingUserId = Guid.NewGuid();

                var service = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationService();
                
                var workflowContext = new Microsoft.Xrm.Sdk.Workflow.Fakes.StubIWorkflowContext();
                workflowContext.PrimaryEntityIdGet = () =>
                {
                    return new Guid("13066B14-0B40-E511-8123-C4346BACFFD0");
                };
                workflowContext.UserIdGet = () =>
                {
                    return workflowUserId;
                };
                workflowContext.CorrelationIdGet = () =>
                {
                    return workflowCorrelationId;
                };
                workflowContext.InitiatingUserIdGet = () =>
                {
                    return workflowInitiatingUserId;
                };

                // ITracingService
                var tracingService = new Microsoft.Xrm.Sdk.Fakes.StubITracingService();
                tracingService.TraceStringObjectArray = (f, o) =>
                {
                    Debug.WriteLine(f, o);
                };

                // IOrganizationServiceFactory
                var factory = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationServiceFactory();
                factory.CreateOrganizationServiceNullableOfGuid = id =>
                {
                    return fi.cube.XrmService;
                };


                Dictionary<string, object> arguments = new Dictionary<string, object>();
                arguments.Add("parameter","test");

                Activity target = (Activity)new WorkflowSample();
                var invoker = new WorkflowInvoker(target);
                invoker.Extensions.Add<ITracingService>(() => tracingService);
                invoker.Extensions.Add<IWorkflowContext>(() => workflowContext);
                invoker.Extensions.Add<IOrganizationServiceFactory>(() => factory);
                IDictionary<string, object> outputs = invoker.Invoke(arguments);

                //Console.WriteLine("SendEmail : {0}, DuplicateContact : {1}, NumberOfDupes : {2}",
                //    outputs["SendEmail"], outputs["DuplicateContact"], outputs["NumberOfDupes"]);
            }
            catch (Exception ex)
            {
                fi.cube.LogSystem.CreateLog(ex.Message);
            }
            finally
            {
                fi.cube.LogSystem.CreateLog("----------------");
            }
        }
    }
}
