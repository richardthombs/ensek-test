FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet test
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /app
COPY --from=build /src/EnsekTest.WebApi/bin/Release/net5.0/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "EnsekTest.WebApi.dll"]
