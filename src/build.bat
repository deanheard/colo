dotnet publish .\Cololight.Console\Cololight.Console.csproj -c Release -f net5.0 -r linux-x64 -p:PublishSingleFile=true --self-contained true
dotnet publish .\Cololight.Console\Cololight.Console.csproj -c Release -f net5.0 -r osx-x64 -p:PublishSingleFile=true --self-contained true
dotnet publish .\Cololight.Console\Cololight.Console.csproj -c Release -f net5.0 -r win-x64 -p:PublishSingleFile=true --self-contained true