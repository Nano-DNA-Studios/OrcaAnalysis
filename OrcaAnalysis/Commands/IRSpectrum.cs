using DNA_CLI_Framework.CommandHandlers;
using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;

namespace OrcaAnalysis.Commands
{
    internal class IRSpectrum : PythonCommand
    {
        public override string CommandName => "IRSpectrum";

        public override string CommandDescription => "Plots the IR Spectrum";

        public override string PythonScriptName => "IRSpectrum";
    }
}
