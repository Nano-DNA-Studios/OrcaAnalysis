using DNA_CLI_Framework.Commands;

namespace OrcaAnalysis.Commands
{
    /// <summary>
    /// Sets Up the Orca Analysis Application
    /// </summary>
    internal class Setup : Command
    {
        /// <summary>
        /// The Orca Docker Image needed to run the Orca Analysis Application
        /// </summary>
        private string _dockerImage = "mrdnalex/orca";

        /// <inheritdoc/>
        public override string CommandName => "Setup";

        /// <inheritdoc/>
        public override string CommandDescription => "Sets up the Orca Analysis Application";

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            ConsoleProcessHandler consoleProcessHandler = new ConsoleProcessHandler(ConsoleProcessHandler.ProcessApplication.CMD);

            consoleProcessHandler.RunProcess($"docker pull {_dockerImage}");
        }
    }
}
