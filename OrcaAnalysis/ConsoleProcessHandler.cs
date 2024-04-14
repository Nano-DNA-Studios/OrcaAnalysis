using System.Diagnostics;

namespace OrcaAnalysis
{
    /// <summary>
    /// Process Handler for the Console Application.
    /// </summary>
    internal class ConsoleProcessHandler
    {
        /// <summary>
        /// Process Start Info
        /// </summary>
        private ProcessStartInfo processStartInfo { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ConsoleProcessHandler()
        {
            processStartInfo = new ProcessStartInfo();

            processStartInfo.FileName = "cmd.exe";
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
        }

        /// <summary>
        /// Runs a Process with the given command.
        /// </summary>
        /// <param name="command"> The Command Passed to the CMD </param>
        public void RunProcess(string command)
        {
            processStartInfo.Arguments = $"/c {command}";

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
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
