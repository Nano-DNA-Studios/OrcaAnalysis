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


### IR Spectrum
Plots the Estimated IR Spectrum Calculated by Orca.

#### Usage
```
orcaanalysis path/to/file/or/folder --IRSpectrum
```

## Creator
Created by [MrDNAlex](https://github.com/MrDNAlex)
