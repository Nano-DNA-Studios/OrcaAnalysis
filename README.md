# Orca Analysis CLI
Orca Analysis CLI is a Command Line tool that helps extracting useful information from Orca files


## Installation
Install the Tool using the following command
```
dotnet tool install --global OrcaAnalysisCLI
```

## Usage

```
orcaanalysis path/to/file/or/folder
```


## Commands
Commands can be Chained onto the default Command, it will overide the displayed information from the regular usage for more specific info. Multiple Commands can be chained along to display more info per usage.

Commands can be used using the Following method

```
orcaanalysis path/to/file/or/folder --CommandName1 commandArg1 commandArg2 ... --CommandName2 commandArg1 commandArg2
```

### Setup
Sets up your device to be ready for all file input cases. At the moment it just downloads a Docker Container from the Web Used for Orca Calculations on your device. (Be wary, the container is ~35GB Large)
This command is meant to not be used with a input path, it should be it's own standalone command.

#### Usage
```
orcaanalysis --Setup
```


### IR Spectrum
Plots the Estimated IR Spectrum Calculated by Orca.

#### Usage
```
orcaanalysis path/to/file/or/folder --IRSpectrum
```


## Creator
Created by [MrDNAlex](https://github.com/MrDNAlex)
