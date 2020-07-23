del /S/Q .\dist\*
dotnet build -c Release
dotnet pack -c Release -o .\dist\