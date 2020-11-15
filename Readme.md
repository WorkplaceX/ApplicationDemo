# Application Demo
A template to get started with ASP.NET Core 5.0 application with Angular 10 client and MS-SQL Server database.

![Build Status](https://github.com/WorkplaceX/ApplicationDemo/workflows/CI/badge.svg) (ApplicationDemo; github actions;)

[![Build Status](https://travis-ci.org/WorkplaceX/ApplicationDemo.svg?branch=master)](https://travis-ci.org/WorkplaceX/ApplicationDemo) (ApplicationDemo; travis;)

## Screenshot
This demo shows the capabilities of the WorkplaceX.org framework. It uses airplanes and a countries stored in the database.
![Screenshot](Application.Doc/Screenshot.png)

## Getting Started
Clone repo and start command line interface.
```cmd
git clone https://github.com/WorkplaceX/ApplicationDemo.git --recursive
cd ApplicationDemo
.\cli.cmd
```
Command line interface CLI contains all necessary framework commands:
![Cli](Application.Doc/Cli.png)

For ConnectionString, deploy sql scripts to database and start the application see: https://workplacex.org/install#git-clone

Or see this "ApplicationDemo" live in action: https://demo.workplacex.org/

## Project Folder and File Structure
* "Application/" (Application with custom business logic in C#)
* "Application.Cli/" (Command line interface to build and deploy in C#)
* "Application.Cli/DeployDb/" (SQL scripts to deploy to SQL server)
* "Application.Database/" (From database generated C# database dto objects like tables and views)
* "Application.Doc/" (Documentation images)
* "Application.Server/" (ASP.NET Core to start application)
* "Application.Website/" (Custom html and css websites used as masters)
* "Framework/" (External WorkplaceX framework)
* "ConfigCli.json" (Configuration file used by Application.Cli command line interface)
* "ConfigServer.json" (Generated configuration used by Application.Server web server)
