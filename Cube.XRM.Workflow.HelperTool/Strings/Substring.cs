using Cube.XRM.Framework;
using Cube.XRM.Framework.AddOn;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace Cube.XRM.Workflow.HelperTool.Strings
{
    public class Substring : WorkflowManager
    {
        [Input("Text")]
        public InArgument<string> pText { get; set; }

        [Output("Result")]
        public OutArgument<string> pResult { get; set; }

        [Input("From Left To Right")]
        [Default("True")]
        public InArgument<bool> LeftToRight { get; set; }

        [Input("Start Index")]
        [Default("0")]
        public InArgument<int> StartIndex { get; set; }

        [Input("Length")]
        [Default("3")]
        public InArgument<int> Length { get; set; }

        /// <summary>
        /// Executes the workflow activity.
        /// </summary>
        /// <param name="cube">The cube.</param>
        protected override void Execute(CubeBase Cube)
        {
            var executionContext = (ActivityContext)Cube.BaseSystemObject;
            string result = PerformSubstring(pText.Get<string>(executionContext), StartIndex.Get<int>(executionContext), Length.Get<int>(executionContext), LeftToRight.Get<bool>(executionContext));
            pResult.Set(executionContext, result);
        }

        private string PerformSubstring(string Result, int Start, int Length, bool LeftToRight)
        {
            if (Start >= 0 && Length >= 0)
            {
                if (!LeftToRight)
                {
                    Start = Result.Length - Length - Start;
                }

                Result = Result.Substring(Start, Length);
            }
            else
                Result = string.Empty;

            return Result;
        }
    }
}
