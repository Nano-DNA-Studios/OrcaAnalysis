using DNA_CLI_Framework.Data;

namespace OrcaAnalysis
{
    /// <summary>
    /// The Data Manager for the Orca Analysis application.
    /// </summary>
    internal class OrcaAnalysisDataManager : DataManager
    {
        /// <inheritdoc/>
        public override string COMMAND_PREFIX => DEFAULT_COMMAND_PREFIX;

        /// <summary>
        /// The Path to the Orca Output File.
        /// </summary>
        public string OrcaOutputPath { get; set; }
    }
}
