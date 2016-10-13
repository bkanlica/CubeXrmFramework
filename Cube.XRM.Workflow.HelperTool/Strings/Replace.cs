using Cube.XRM.Framework;
using Cube.XRM.Framework.AddOn;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Text.RegularExpressions;

namespace Cube.XRM.Workflow.HelperTool.Strings
{
    public class Replace : WorkflowManager
    {
        [Input("Text")]
        public InArgument<string> pText { get; set; }

        [Output("Result")]
        public OutArgument<string> pResult { get; set; }

        [Input("Existing Value")]
        public InArgument<string> pExistingValue { get; set; }

        [Input("New Value")]
        public InArgument<string> pNewValue { get; set; }

        [Input("isCaseSensitive")]
        [Default("False")]
        public InArgument<bool> pCaseSensitive { get; set; }

        /// <summary>
        /// Executes the workflow activity.
        /// </summary>
        /// <param name="cube">The cube.</param>
        protected override void Execute(CubeBase Cube)
        {
            var executionContext = (ActivityContext)Cube.BaseSystemObject;

            string text = pText.Get<string>(executionContext);
            string existingValue = pExistingValue.Get<string>(executionContext);

            var newValue = pNewValue.Get<string>(executionContext);
            if (newValue == null) newValue = String.Empty;

            var result = string.Empty;
            if (!pCaseSensitive.Get<bool>(executionContext))
            {
                if (!String.IsNullOrEmpty(text) && !String.IsNullOrEmpty(existingValue))
                {
                    result = text.Replace(existingValue, newValue);
                }
            }
            else
            {
                var regex = new Regex(existingValue, RegexOptions.IgnoreCase);
                result = regex.Replace(text, newValue);
            }

            pResult.Set(executionContext, result);
        }
    }
}
