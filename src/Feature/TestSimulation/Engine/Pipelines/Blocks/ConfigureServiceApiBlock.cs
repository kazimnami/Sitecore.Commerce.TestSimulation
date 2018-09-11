using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Builder;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Sitecore.Commerce.Core.Commands;

namespace Feature.TestSimulation.Engine
{
    /// <summary>
    /// Configure services
    /// </summary>
    /// <seealso cref="PipelineBlock{ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext}" />
    [PipelineDisplayName("TestSimulation.block.dc.configureserviceapi")]
    public class ConfigureServiceApiBlock : PipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        /// <summary>
        /// Runs the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="context">The context.</param>
        /// <returns>model builder</returns>
        public override Task<ODataConventionModelBuilder> Run(ODataConventionModelBuilder modelBuilder, CommercePipelineExecutionContext context)
        {
            Condition.Requires(modelBuilder).IsNotNull("The argument can not be null");

            var setupAction = modelBuilder.Action("TestSimulationSetup");
            setupAction.Parameter<string>("cartId");
            setupAction.ReturnsFromEntitySet<CommerceCommand>("Commands");

            return Task.FromResult(modelBuilder);
        }


    }
}
