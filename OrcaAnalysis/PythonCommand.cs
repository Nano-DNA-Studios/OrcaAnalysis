using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using System.Reflection;

namespace OrcaAnalysis
{
    /// <summary>
    /// Represents a Command that Executes a Python Script
    /// </summary>
    abstract class PythonCommand : Command
    {
        /// <summary>
        /// The Name of the Python Script to Execute (Do not include the .py Extension)
        /// </summary>
        public abstract string PythonScriptName { get; }

        /// <summary>
        /// The Note to Display when Running the Script
        /// </summary>
        public abstract string RunScriptNote { get; }

        /// <summary>
        /// Data Manager for the Orca Analysis Commands
        /// </summary>
        OrcaAnalysisDataManager Data => ApplicationData<OrcaAnalysisDataManager>.Instance();

        /// <summary>
        /// Gets the Path to the Assembly that is being Executed, this is wherer we can Access the Added Resources and Files from the Application
        /// </summary>
        /// <returns></returns>
        private string GetAssemblyDirectory()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            string assemblyLocation = executingAssembly.Location;

            return Path.GetDirectoryName(assemblyLocation);
        }

        /// <summary>
        /// Executes the Python Script
        /// </summary>
        /// <param name="args"> Arguments Associated with the  </param>
        public override void Execute(string[] args)
        {
            RunPythonScript();

            if (args.Length == 0)
            {
                return;
            }
        }

        /// <summary>
        /// Runs the Python Script
        /// </summary>
        private void RunPythonScript()
        {
            ConsoleProcessHandler processHandler = new ConsoleProcessHandler();

            string pythonFile = Path.Combine(GetAssemblyDirectory(), $"Scripts/{PythonScriptName}.py");

            Console.WriteLine(RunScriptNote + Data.OrcaOutputPath);

            processHandler.RunProcess($"python {pythonFile} {Data.OrcaOutputPath}");
        }
    }
}
