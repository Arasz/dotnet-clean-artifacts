# dotnet-clean-artifacts
[![Build and publish package](https://github.com/Arasz/dotnet-clean-artifacts/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Arasz/dotnet-clean-artifacts/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/Arasz/dotnet-clean-artifacts/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/Arasz/dotnet-clean-artifacts/actions/workflows/github-code-scanning/codeql)

Global .NET Core tool that can clean all your build artifacts. It will look for "bin" and "obj" directories and remove them. 

## How to get it?
From [nuget](https://www.nuget.org/packages/dotnet-clean-artifacts)

## Installation
Execute
```
dotnet tool install -g dotnet-clean-artifacts
```

## How to run it?

Run in your project main directory:
```
clean-artifacts
```
To see all available options run:
```
clean-artifacts -h
```
