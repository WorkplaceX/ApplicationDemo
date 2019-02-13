# Application
[![Build Status](https://travis-ci.org/WorkplaceX/Application.svg?branch=master)](https://travis-ci.org/WorkplaceX/Application)
(Application; travis;)

Develop business applications with WorkplaceX framework.

## Clone
```cmd
git clone https://github.com/WorkplaceX/ApplicationDemo.git --recursive
```
Recursive argument enables additional cloning of submodule (Framework).

## Command Line Interface (CLI)
In the root folder type cli for framework cli.

```cmd
cd ApplicationDemo
.\cli.cmd
```
This will show the framework CLI:

![Cli](https://raw.githubusercontent.com/WorkplaceX/Framework/master/Doc/Cli.png)

## ConnectionString
Set ConnectionString with cli:
```cmd
.\cli.cmd config ConnectionString="Data Source=localhost; Initial Catalog=Application; Integrated Security=True;"
```

## Generate
Transform database schema to C# code. Creates one object for every table and view and column.
```cmd
.\cli.cmd generate
```

## .travis.yml
File is a copy of Framework/Framework.Cli/Ci/travis-ci.org
