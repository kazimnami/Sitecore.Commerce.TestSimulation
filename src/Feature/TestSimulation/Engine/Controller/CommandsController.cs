using Microsoft.AspNetCore.Mvc;
using Sitecore.Commerce.Core;
using System;
using System.Threading.Tasks;
using System.Web.Http.OData;

namespace Feature.TestSimulation.Engine
{
    /// <summary>
    /// Commands Controller
    /// </summary>
    /// <seealso cref="CommerceController" />
    public class CommandsController : CommerceController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsController"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="globalEnvironment">The global environment.</param>
        public CommandsController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) : base(serviceProvider, globalEnvironment)
        {
        }

        /// <summary>
        /// Test Simulation Set Up Action
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("TestSimulationSetup()")]
        public async Task<IActionResult> TestSimulationSetup([FromBody] ODataActionParameters value)
        {
            if (!ModelState.IsValid || value == null)
                return (IActionResult)new BadRequestObjectResult(this.ModelState);

            if (!value.ContainsKey("cartId"))
                return (IActionResult)new BadRequestObjectResult((object)value);

            var cartId = value["cartId"].ToString();
            var command = Command<TestSimulationCommand>();
            var result = await command.Process(this.CurrentContext, cartId);

            if (result == null)
                return (IActionResult)new BadRequestObjectResult($"Cart : {cartId} was not found.");

            return (IActionResult)new ObjectResult((object)command);
        }

    }
}
