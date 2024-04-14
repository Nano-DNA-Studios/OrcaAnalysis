using DNA_CLI_Framework.Data;
using DNA_CLI_Framework;

namespace OrcaAnalysis
{
    /// <summary>
    /// Class that represents the Orca Analysis application.
    /// </summary>
    /// <typeparam name="T"> The Data Manager Type </typeparam>
    internal class OrcaAnalysisApp<T> : CLIApplication<T> where T : DataManager, new()
    {
        /// <inheritdoc/>
        public override string ApplicationName => "Orca Analysis";

        public OrcaAnalysisApp() : base()
        {
        }
    }
}
