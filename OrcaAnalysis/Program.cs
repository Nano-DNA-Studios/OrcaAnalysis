using DNA_CLI_Framework.CommandHandlers;

namespace OrcaAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrcaAnalysisApp<OrcaAnalysisDataManager> orcaApp = new OrcaAnalysisApp<OrcaAnalysisDataManager>();
            orcaApp.SetCommandHandler<FileOrDirectoryCommandHandler>();
            orcaApp.RunApplication(args);
        }
    }
}
