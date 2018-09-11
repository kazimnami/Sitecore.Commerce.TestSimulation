using Sitecore.Commerce.Core;

namespace Foundation.TestSimulation.Engine
{
    /// <summary>
    /// Test Simulation Component
    /// </summary>
    /// <seealso cref="Component" />
    public class TestSimulationComponent : Component
    {
        /// <summary>
        /// Gets or sets the status for test simulation.
        /// </summary>
        /// <value>
        /// the status for test simulation.
        /// </value>
        public bool Status { get; set; }
    }
}