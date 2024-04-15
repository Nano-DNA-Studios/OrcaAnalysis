
namespace OrcaAnalysis.Commands
{
    /// <summary>
    /// Command to Analyze the IR Spectrum of an Orca Output File
    /// </summary>
    internal class IRSpectrum : PythonCommand
    {
        /// <inheritdoc/>
        public override string CommandName => "IRSpectrum";

        /// <inheritdoc/>
        public override string CommandDescription => "Plots the IR Spectrum";

        /// <inheritdoc/>
        public override string PythonScriptName => "IRSpectrum";

        /// <inheritdoc/>
        public override string RunScriptNote => "Analyzing IR Spectrum for : ";
    }
}
