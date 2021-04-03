# Application Demo
An ASP.NET Core 5.0 application with Angular 11 and MS-SQL Server. See it live in action: https://demo.workplacex.org/

![Build Status](https://github.com/WorkplaceX/ApplicationDemo/workflows/CI/badge.svg) (ApplicationDemo; github actions;)

[![Build Status](https://travis-ci.org/WorkplaceX/ApplicationDemo.svg?branch=master)](https://travis-ci.org/WorkplaceX/ApplicationDemo) (ApplicationDemo; travis;)

# Getting Started
The following two components need to be installed on the machine as prerequisite (Windows or Linux):
* [Node.js](https://nodejs.org/en/) (LTS Version)
* [.NET Core](https://dotnet.microsoft.com/download) (Version 5.0)
* [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

# Install
```cmd
### Git clone (parameter recursive clones also required submodule Framework):
git clone https://github.com/WorkplaceX/ApplicationDemo.git --recursive

cd ApplicationDemo

### On first launch it will ask to register wpx command:
./wpx.cmd # For Windows
./wpx.sh # For Linux

### From now on just use:
wpx

### Set ConnectionString
wpx config connectionString="Data Source=localhost; Initial Catalog=ApplicationDemo; Integrated Security=True;" # Example Windows
wpx config connectionString="Data Source=localhost; Initial Catalog=ApplicationDemo; User Id=SA; Password=MyPassword;" # Example Linux

### Deploy Database
wpx deployDb

### Start
wpx start # http://localhost:5000/
```

## Screenshot
This demo shows the capabilities of the WorkplaceX.org framework. It displays airplanes and countries stored in the database.
![Screenshot](Application.Doc/Screenshot.png)

Command line interface CLI contains all necessary framework commands:
![Cli](Application.Doc/Cli.png)
