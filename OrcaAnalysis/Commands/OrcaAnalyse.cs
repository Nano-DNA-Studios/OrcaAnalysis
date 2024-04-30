using DNA_CLI_Framework.Commands;
using DNA_CLI_Framework.Data;

namespace OrcaAnalysis.Commands
{
    internal class OrcaAnalyse : DefaultCommand
    {
        private const string CACHEFOLDER = "OrcaCache";

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

        /// <inheritdoc/>
        public override void Execute(string[] args)
        {
            ExtractFilePath(args);
        }

        /// <inheritdoc/>
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
                Console.WriteLine($"Invalid File Path, Stopping Program ({fullPath})");
                return;
            }

            string relativePath = Path.GetRelativePath(Directory.GetCurrentDirectory(), fullPath);

            if (Path.GetFileName(relativePath).Contains(TAR))
            {
                Console.WriteLine($"Extracting TAR File {relativePath}");

                ConsoleProcessHandler commandHandler = new ConsoleProcessHandler();

                if (Directory.Exists(CACHEFOLDER))
                    commandHandler.RunProcess($"rm -rf {CACHEFOLDER}");

                commandHandler.RunProcess($"mkdir Output");
                commandHandler.RunProcess($"tar -xzf {relativePath} -C {CACHEFOLDER}");

                relativePath = FindOutputFile(CACHEFOLDER);
            }

            if (Path.GetExtension(relativePath) == OUTPUT)
                Data.OrcaOutputPath = relativePath;
            else
            {
                Console.WriteLine("Invalid File Type");
                return;
            }
        }
    }
}
