using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;
using System.ComponentModel.Design;
using System.Reflection;

namespace OrcaAnalysis.Commands
{
    internal class OrcaAnalyse : DefaultCommand
    {
        /// <summary>
        /// The File Extension for TAR Files / Archive Files
        /// </summary>
        private const string TAR = ".tar.gz";

        /// <summary>
        /// The File Extension for Orca Output Files
        /// </summary>
        private const string OUTPUT = ".out";

        /// <summary>
        /// Data Manager for the Orca Analysis Commands
        /// </summary>
        OrcaAnalysisDataManager Data => ApplicationData<OrcaAnalysisDataManager>.Instance();


        public override void Execute(string[] args)
        {
            ExtractFilePath(args);
        }

        public override void ExecuteSolo(string[] args)
        {
            ExtractFilePath(args);
        }

        /// <summary>
        /// Finds the Output File in the Directory and Subdirectories.
        /// </summary>
        /// <param name="path"> The Path to Start the Search </param>
        /// <returns> The Relative Path from the File, or an Empty string if not found </returns>
        private string FindOutputFile(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file.Contains(OUTPUT))
                    return file;
            }

            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                string foundFile = FindOutputFile(directory);
                if (!string.IsNullOrEmpty(foundFile))
                    return foundFile; 
            }

            return string.Empty;
        }

        /// <summary>
        /// Extracts the File Path from the Arguments, Extracts the TAR File if Necessary
        /// </summary>
        /// <param name="args"> Arguments passed to the Program </param>
        private void ExtractFilePath(string[] args)
        {
            string fullPath = Path.GetFullPath(args[0]);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Invalid File Path, Stopping Program");
                return;
            }

            Data.FilePath = args[0];

            if (Path.GetFileName(Data.FilePath).Contains(TAR))
            {
                Console.WriteLine("Extracting TAR File...");

                ConsoleProcessHandler commandHandler = new ConsoleProcessHandler();

                if (Directory.Exists("Output"))
                    commandHandler.RunProcess("rm -rf Output");

                commandHandler.RunProcess($"mkdir Output");
                commandHandler.RunProcess($"tar -xzf {args[0]} -C Output");

                Data.FilePath = FindOutputFile("Output");
            }

            if (Path.GetExtension(Data.FilePath) == OUTPUT)
                Data.OrcaOutputPath = Data.FilePath;
            else
            {
                Console.WriteLine("Invalid File Type");
                return;
            }
        }
    }
}
