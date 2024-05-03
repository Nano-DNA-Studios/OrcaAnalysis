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
        /// The File Extension for Orca Input Files
        /// </summary>
        private const string INPUT = ".inp";

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

            if (Path.GetExtension(relativePath) == INPUT)
                relativePath = RunCalculation(relativePath);

            if (Path.GetFileName(relativePath).Contains(TAR))
                relativePath = ExtractTarFile(relativePath);

            if (Path.GetExtension(relativePath) == OUTPUT)
                Data.OrcaOutputPath = relativePath;
            else
            {
                Console.WriteLine("Invalid File Type");
                return;
            }
        }

        /// <summary>
        /// Runs a Docker Container that will run a Orca Calculation. Once complete, the Output File will be returned.
        /// </summary>
        /// <param name="relativePath"> The Relative Path to the Input File </param>
        /// <returns> The Path to the Output File </returns>
        private string RunCalculation(string relativePath)
        {
            Console.WriteLine($"Running Orca Calculation on {relativePath}");

            ConsoleProcessHandler commandHandler = new ConsoleProcessHandler(ConsoleProcessHandler.ProcessApplication.PowerShell);

            string fileName = Path.GetFileNameWithoutExtension(relativePath);
            string containerName = $"orca_{fileName.ToLower()}";
            string dockerRunCommand = "docker run";
            string cacheVolume1 = $"-v '{Directory.GetCurrentDirectory()}/OrcaCache:/home/orca/calculationData'";
            string cacheVolume2 = $"-v '{Directory.GetCurrentDirectory()}:/data'";
            string setContainerName = $"--name {containerName}";
            string dockerImage = "mrdnalex/orca";
            string bashCommand = "/bin/bash -c";
            string createFolder = $"mkdir /home/orca/calculationData/{fileName}";
            string copyFile = $"cp /data/{relativePath.Replace("\\", "/")} /home/orca/calculationData/{fileName}";
            string runOrca = $"/Orca/orca /home/orca/calculationData/{fileName}/{fileName}.inp 2>&1 | tee -a /home/orca/calculationData/{fileName}/{fileName}.out";
            string fullCommand = $"{dockerRunCommand} {cacheVolume1} {cacheVolume2} {setContainerName} {dockerImage} {bashCommand} '{createFolder} && {copyFile} && {runOrca}'";

            if (Directory.Exists($"{CACHEFOLDER}/{fileName}"))
                Directory.Delete($"{CACHEFOLDER}/{fileName}", true);

            commandHandler.RunProcess(fullCommand);
            commandHandler.RunProcess($"docker rm {containerName}");

            return FindOutputFile($"{CACHEFOLDER}/{fileName}");
        }

        /// <summary>
        /// Extracts the TAR File and it's contents to the Cache Folder
        /// </summary>
        /// <param name="relativePath"> The relative Path to the TAR File </param>
        /// <returns> The Relative Path to the Extracted Output File </returns>
        private string ExtractTarFile(string relativePath)
        {
            Console.WriteLine($"Extracting TAR File {relativePath}");

            ConsoleProcessHandler commandHandler = new ConsoleProcessHandler(ConsoleProcessHandler.ProcessApplication.CMD);

            string extractionPath = Path.Combine(CACHEFOLDER, "Extract");

            if (!Directory.Exists(CACHEFOLDER))
                commandHandler.RunProcess($"mkdir {CACHEFOLDER}");

            if (Directory.Exists(extractionPath))
                Directory.Delete(extractionPath, true);

            commandHandler.RunProcess($"mkdir {extractionPath}");
            commandHandler.RunProcess($"tar -xzf {relativePath} -C {extractionPath}");

            return FindOutputFile(extractionPath);
        }
    }
}
