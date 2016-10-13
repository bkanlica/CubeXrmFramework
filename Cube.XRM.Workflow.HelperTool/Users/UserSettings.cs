using Cube.XRM.Framework;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.XRM.Workflow.HelperTool.Users
{
    public static class UserSettings
    {
        public static string FetchXML =@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' >
                            <entity name = 'usersettings' >
                                < filter type='and' >
                                    <condition attribute = 'systemuserid' operator='eq' value='{0}' />
                                </filter>
                            </entity>
                        </fetch>";

        public static Entity GetUserSettings(Guid UserID, CubeBase Cube)
        {
            Result result = Cube.RetrieveActions.getItemFetch(String.Format(FetchXML, UserID));
            if (result.isError)
                throw new Exception(result.Message);

            return (Entity)result.BusinessObject;
        }
    }
}
