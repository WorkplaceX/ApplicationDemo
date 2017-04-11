cd Build
/home/travis/.dotnet restore
/home/travis/.dotnet build
/home/travis/.dotnet run 01 $ConnectionString
/home/travis/.dotnet run 02
