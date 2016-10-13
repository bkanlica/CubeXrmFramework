using Cube.XRM.Framework;
using Cube.XRM.Framework.AddOn;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.XRM.Workflow.HelperTool
{
    public class Common : WorkflowManager
    {
        [Input("Lookup Field Name")]
        public InArgument<string> parameter { get; set; }

        /// <summary>
        /// Executes the workflow activity.
        /// </summary>
        /// <param name="cube">The cube.</param>
        protected override void Execute(CubeBase cube)
        {
            Result r = cube.XRMActions.Execute<CreateRequest, CreateResponse>(new CreateRequest() { });
        }
    }
}
