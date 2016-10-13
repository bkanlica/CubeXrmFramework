using Cube.XRM.Framework;
using Cube.XRM.Framework.AddOn;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;
using System.Web;

namespace Cube.XRM.Workflow.HelperTool.Strings
{
    public class URLEncodeDecode : WorkflowManager
    {
        [Input("isEncode")]
        public InArgument<bool> pOperationType { get; set; }

        [Input("Text")]
        public InArgument<string> pText { get; set; }

        [Output("Result")]
        public OutArgument<string> pResult { get; set; }

        /// <summary>
        /// Executes the workflow activity.
        /// </summary>
        /// <param name="Cube">The cube.</param>
        protected override void Execute(CubeBase Cube)
        {
            string Text = pText.Get<string>((ActivityContext)Cube.BaseSystemObject);
            bool OperationType = pOperationType.Get<bool>((ActivityContext)Cube.BaseSystemObject);

            string Result = string.Empty;

            if (OperationType)
                Result = HttpUtility.UrlEncode(Text);
            else
                Result = HttpUtility.UrlDecode(Text);

            pResult.Set((ActivityContext)Cube.Context, Result);
        }
    }
}
