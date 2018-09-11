using Foundation.TestSimulation.Engine;
using Sitecore.Commerce.Plugin.Carts;

namespace Feature.TestSimulation.Engine
{
    using System.Threading.Tasks;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Core.Commands;


    /// <summary>
    /// Defines the Test Simulation command.
    /// </summary>
    public class TestSimulationCommand : CommerceCommand
    {
        /// <summary>
        /// The get cart pipeline
        /// </summary>
        private readonly GetCartPipeline _getCartPipeline;
        private readonly IPersistEntityPipeline _persistEntityPipeline;

        /// <summary>
        /// Test simulation command constructor.
        /// </summary>
        /// <param name="getCartPipeline">The get cart pipeline.</param>
        /// <param name="persistEntityPipeline"></param>
        public TestSimulationCommand(GetCartPipeline getCartPipeline, IPersistEntityPipeline persistEntityPipeline)
        {
            _getCartPipeline = getCartPipeline;
            _persistEntityPipeline = persistEntityPipeline;
        }
        /// <summary>
        /// The process of the command
        /// </summary>
        /// <param name="commerceContext">
        /// The commerce context
        /// </param>
        /// <param name="cartId"></param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<Cart> Process(CommerceContext commerceContext, string cartId)
        {
            using (var activity = CommandActivity.Start(commerceContext, this))
            {
                var arg = new ResolveCartArgument(commerceContext.CurrentShopName(), cartId,
                    commerceContext.CurrentShopperId());

                var cart = await _getCartPipeline.Run(arg, commerceContext.GetPipelineContextOptions());

                var cartComponent = cart.GetComponent<TestSimulationComponent>();
                cartComponent.Status = true;

                var persistEntityArgument = await this._persistEntityPipeline.Run(new PersistEntityArgument(cart), commerceContext.GetPipelineContextOptions());
                return cart;
            }
        }
    }
}