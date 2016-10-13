using Cube.XRM.Framework;
using Cube.XRM.Framework.AddOn;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Globalization;

namespace Cube.XRM.Workflow.HelperTool.Date
{
    public class ParseDate : WorkflowManager
    {
        [Input("Date")]
        [Default("1900-01-01T00:00:00Z")]
        public InArgument<DateTime> pDate { get; set; }

        [Input("Date Format")]
        [Default("MM/dd/yyyy")]
        public InArgument<string> pDateFormat { get; set; }

        [Output("Day Of Week")]
        public OutArgument<int> pDayOfWeek { get; set; }

        [Output("Day Of Week (Text)")]
        public OutArgument<string> pDayOfWeekString { get; set; }

        [Output("Day Of Year")]
        public OutArgument<int> pDayOfYear { get; set; }

        [Output("Week Number")]
        public OutArgument<int> pWeek { get; set; }

        [Output("Year")]
        public OutArgument<int> pYear { get; set; }

        [Output("Month")]
        public OutArgument<int> pMonth { get; set; }

        [Output("Month (Text)")]
        public OutArgument<string> MonthText { get; set; }

        [Output("Day")]
        public OutArgument<int> pDay { get; set; }

        [Output("Hour")]
        public OutArgument<int> pHour { get; set; }

        [Output("Minute")]
        public OutArgument<int> pMinute { get; set; }

        protected override void Execute(CubeBase Cube)
        {
            var executionContext = (ActivityContext)Cube.BaseSystemObject;

            var dateFormat = pDateFormat.Get<string>(executionContext);
            DateTime date = pDate.Get<DateTime>(executionContext);
            

            pYear.Set(executionContext, date.Year);
            pMonth.Set(executionContext, date.Month);
            pDay.Set(executionContext, date.Day);
            pHour.Set(executionContext, date.Hour);

            var cultureInfo = GetCultureInfo(executionContext, LanguageCode.Get<int>(executionContext));
            MonthText.Set(executionContext, date.ToString("MMMM", cultureInfo.DateTimeFormat));
            pDayOfWeekString.Set(executionContext, date.ToString("dddd", cultureInfo.DateTimeFormat));

            pHour.Set(executionContext, date.Hour);
            pMinute.Set(executionContext, date.Minute);

            pDayOfWeek.Set(executionContext, (int)date.DayOfWeek);
            pDayOfYear.Set(executionContext, date.DayOfYear);
            pWeek.Set(executionContext, GetWeek(date));
        }

        private static CultureInfo GetCultureInfo(CodeActivityContext executionContext, int languageCode)
        {
            if (languageCode > 0) return new CultureInfo(languageCode);
            var settings = UserSettings.GetUserSettings(executionContext);
            var uilang = (int)settings["uilanguageid"];
            return new CultureInfo(uilang);
        }

    }
}
