using Cube.XRM.Framework;
using Cube.XRM.Framework.AddOn;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace Cube.XRM.Workflow.HelperTool.Strings
{
    public class StringLength : WorkflowManager
    {
        [Input("Text")]
        public InArgument<string> pText { get; set; }

        [Output("Result")]
        public OutArgument<string> pResult { get; set; }

        /// <summary>
        /// Executes the workflow activity.
        /// </summary>
        /// <param name="cube">The cube.</param>
        protected override void Execute(CubeBase Cube)
        {
            var executionContext = (ActivityContext)Cube.BaseSystemObject;

            pResult.Set(executionContext, pText.Get<string>(executionContext).Length);
        }
    }
}
