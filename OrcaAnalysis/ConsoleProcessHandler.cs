using System.Diagnostics;

namespace OrcaAnalysis
{
    /// <summary>
    /// Process Handler for the Console Application.
    /// </summary>
    internal class ConsoleProcessHandler
    {
        /// <summary>
        /// The Application that will handle the Process.
        /// </summary>
        public enum ProcessApplication
        {
            CMD,
            PowerShell,
            Other
        }

        /// <summary>
        /// Process Start Info
        /// </summary>
        private ProcessStartInfo _processStartInfo { get; set; }

        /// <summary>
        /// The Application that will handle the Process.
        /// </summary>
        public ProcessApplication Application { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ConsoleProcessHandler(ProcessApplication application)
        {
            _processStartInfo = new ProcessStartInfo();

            Application = application;
            _processStartInfo.FileName = GetApplicationPath(application);
            _processStartInfo.RedirectStandardOutput = true;
            _processStartInfo.RedirectStandardError = true;
            _processStartInfo.CreateNoWindow = true;
            _processStartInfo.UseShellExecute = false;
        }

        /// <summary>
        /// Returns the File Name / Executable of the Application based on the ProcessApplication Enum.
        /// </summary>
        /// <param name="application"> The Application Selected </param>
        /// <returns> The Executable / File Name of the Application </returns>
        private string GetApplicationPath(ProcessApplication application)
        {
            switch (application)
            {
                case ProcessApplication.CMD:
                    return "cmd.exe";
                case ProcessApplication.PowerShell:
                    return "powershell.exe";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Returns the Arguments for the Application based on the ProcessApplication Enum.
        /// </summary>
        /// <param name="application"> Application that will run the Process </param>
        /// <param name="command"> The Commands to run </param>
        /// <returns> The Arguments that will be passed to the Application </returns>
        private string GetApplicationArguments(ProcessApplication application, string command)
        {
            switch (application)
            {
                case ProcessApplication.CMD:
                    return $"/c {command}";
                case ProcessApplication.PowerShell:
                    return $"-Command {command}";
                default:
                    return command;
            }
        }

        /// <summary>
        /// Runs a Process with the given command.
        /// </summary>
        /// <param name="command"> The Command Passed to the CMD </param>
        public void RunProcess(string command)
        {
            _processStartInfo.Arguments = GetApplicationArguments(Application, command);

            using (Process process = new Process())
            {
                process.StartInfo = _processStartInfo;
                process.Start();

                while (!process.StandardOutput.EndOfStream)
                    Console.WriteLine(process.StandardOutput.ReadLine());

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine(process.StandardError.ReadToEnd());
                }
            }
        }
    }
}
