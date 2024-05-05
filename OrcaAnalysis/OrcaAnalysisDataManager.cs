using DNA_CLI_Framework.Data;

namespace OrcaAnalysis
{
    /// <summary>
    /// The Data Manager for the Orca Analysis application.
    /// </summary>
    internal class OrcaAnalysisDataManager : DataManager
    {
        /// <summary>
        /// 
        /// </summary>
        public string CacheFolder { get; private set; } = "OrcaCache";

        /// <inheritdoc/>
        public override string COMMAND_PREFIX => DEFAULT_COMMAND_PREFIX;

        /// <summary>
        /// The Path to the Orca Output File.
        /// </summary>
        public string OrcaOutputPath { get; set; }

        /// <summary>
        /// The Path to the Orca Input File.
        /// </summary>
        public string OrcaInputPath { get; set; }

        /// <summary>
        /// The Path to the Orca Compressed File. (TAR)
        /// </summary>
        public string OrcaCompressedPath { get; set; }
    }
}
