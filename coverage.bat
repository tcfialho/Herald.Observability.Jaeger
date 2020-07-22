dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=\"json,opencover\" /p:CoverletOutput="../coverage/" /p:Exclude="[*.Test*.*]*"
reportgenerator "-reports:coverage/coverage.opencover.xml" "-targetdir:coverage"
start "" "coverage\index.htm"