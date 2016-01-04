// ***********************************************************************
// Assembly         : Cube.XRM.Framework.Core
// Author           : Baris Kanlica
// Created          : 09-21-2015
//
// Last Modified By : Baris Kanlica
// Last Modified On : 09-17-2015
// ***********************************************************************
// <copyright file="CRMSettingReader.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation 2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;

/// <summary>
/// The Settings namespace.
/// </summary>
namespace Cube.XRM.Framework.Core.Settings
{
    /// <summary>
    /// Class CRMSettingReader.
    /// </summary>
    [EntityLogicalName("mb_globalsetting")]
    class CRMSettingReader : Entity, ISettingReader
    {
        /// <summary>
        /// The entity name
        /// </summary>
        public const string EntityName = "mb_globalsetting";

        /// <summary>
        /// Initializes a new instance of the <see cref="CRMSettingReader" /> class.
        /// </summary>
        public CRMSettingReader()
        {
            this.LogicalName = EntityName;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get
            {
                return this.Attributes.Contains("mb_name") ? this.Attributes["mb_name"].ToString() : null;
            }
            set
            {
                this.Attributes["mb_name"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return this.Attributes.Contains("mb_value") ? this.Attributes["mb_value"].ToString() : null;
            }
            set
            {
                this.Attributes["mb_value"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        public string Group
        {
            get
            {
                return this.Attributes.Contains("mb_group") ? this.Attributes["mb_group"].ToString() : null;
            }
            set
            {
                this.Attributes["mb_group"] = value;
            }
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>List&lt;SettingGroup&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<SettingGroup> Read()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the specified setting file extension.
        /// </summary>
        /// <param name="SettingFileExtension">The setting file extension.</param>
        /// <returns>List&lt;SettingGroup&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<SettingGroup> Read(string SettingFileExtension)
        {
            throw new NotImplementedException();
        }

        //public static string RetrieveValue(string key, string group, IOrganizationService organizationService)
        //{
        //    if (string.IsNullOrEmpty(key))
        //        throw new ArgumentNullException("Key parameter cannot be null or empty");

        //    QueryExpression query = new QueryExpression("mb_globalsetting");
        //    query.NoLock = true;
        //    query.ColumnSet.AddColumns(new string[] { "mb_value" });
        //    query.Criteria.AddCondition(new ConditionExpression("mb_name", ConditionOperator.Equal, key));
        //    query.Criteria.AddCondition(new ConditionExpression("statecode", ConditionOperator.Equal, 0)); //Active
        //    if (!string.IsNullOrEmpty(group))
        //        query.Criteria.AddCondition(new ConditionExpression("mb_group", ConditionOperator.Equal, group));

        //    EntityCollection resultEntityCollection = organizationService.RetrieveMultiple(query);

        //    if (resultEntityCollection.Entities.Count == 0)
        //        throw new Exception(string.Format("There is not an active Global Setting record linked to the requested key: {0}", key));
        //    else if (resultEntityCollection.Entities.Count > 1)
        //        throw new Exception(string.Format("There is more than one Global Setting record linked to the requested key: {0}", key));
        //    else
        //        return resultEntityCollection.Entities[0].Contains("mb_value") ? resultEntityCollection.Entities[0]["mb_value"].ToString() : string.Empty;
        //}

        //public static EntityCollection RetrieveValuesByGroup(string group, IOrganizationService organizationService)
        //{
        //    if (string.IsNullOrEmpty(group))
        //        throw new ArgumentNullException("Group parameter cannot be null or empty");

        //    QueryExpression query = new QueryExpression("mb_globalsetting");
        //    query.NoLock = true;
        //    query.ColumnSet.AddColumns(new string[] { "mb_name", "mb_value", "mb_securevalue" });
        //    query.Criteria.AddCondition(new ConditionExpression("statecode", ConditionOperator.Equal, 0)); //Active
        //    query.Criteria.AddCondition(new ConditionExpression("mb_group", ConditionOperator.Equal, group));

        //    return organizationService.RetrieveMultiple(query);
        //}

        //public static string GetSetting(string Name, IOrganizationService Service)
        //{
        //    string setting = String.Empty;

        //    QueryExpression query = new QueryExpression("mb_globalsetting");
        //    query.NoLock = true;
        //    query.ColumnSet = new ColumnSet(new string[] { "mb_name", "mb_value", "mb_securevalue" });
        //    query.Criteria.AddCondition("mb_name", ConditionOperator.Equal, Name);

        //    EntityCollection data = Service.RetrieveMultiple(query);
        //    if (data != null && data.Entities.Count > 0 && data.Entities[0].Contains("mb_value"))
        //    {
        //        setting = Convert.ToString(data.Entities[0]["mb_value"]);
        //    }

        //    return setting;
        //}

        //public static string GetSecureSetting(string Name, IOrganizationService Service)
        //{
        //    string setting = String.Empty;

        //    QueryExpression query = new QueryExpression("mb_globalsetting");
        //    query.NoLock = true;
        //    query.ColumnSet = new ColumnSet(new string[] { "mb_securevalue" });
        //    query.Criteria.AddCondition("mb_name", ConditionOperator.Equal, Name);

        //    EntityCollection data = Service.RetrieveMultiple(query);
        //    if (data != null && data.Entities.Count > 0 && data.Entities[0].Contains("mb_securevalue"))
        //    {
        //        setting = Convert.ToString(data.Entities[0]["mb_securevalue"]);
        //    }

        //    return setting;
        //}
    }
}
