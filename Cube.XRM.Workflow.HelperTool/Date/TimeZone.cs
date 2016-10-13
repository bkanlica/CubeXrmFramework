using Cube.XRM.Framework;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.XRM.Workflow.HelperTool.Date
{
    public static class TimeZone
    {
        public static Result RetrieveLocalTimeFromUTCTime(DateTime utcTime, int timeZoneCode, CubeBase Cube)
        {
            var request = new LocalTimeFromUtcTimeRequest
            {
                TimeZoneCode = timeZoneCode,
                UtcTime = utcTime.ToUniversalTime()
            };

            Result result = Cube.XRMActions.Execute<LocalTimeFromUtcTimeRequest, LocalTimeFromUtcTimeResponse>(request);

            return result;
        }

        public static Result RetrieveUTCTimeFromLocalTime(DateTime localTime, int timeZoneCode, CubeBase Cube)
        {

            var request = new UtcTimeFromLocalTimeRequest
            {
                TimeZoneCode = timeZoneCode,
                LocalTime = localTime
            };

            Result result = Cube.XRMActions.Execute<UtcTimeFromLocalTimeRequest, UtcTimeFromLocalTimeResponse>(request);

            return result;
        }
    }
}