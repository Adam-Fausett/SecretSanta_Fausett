#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY ["SecretSanta/SecretSanta.csproj", "SecretSanta/"]
COPY ["SecretSanta.Core/SecretSanta.Core.csproj", "SecretSanta.Core/"]
COPY ["SecretSanta.Models/SecretSanta.Models.csproj", "SecretSanta.Models/"]
COPY ["SecretSanta.Tests/SecretSanta.Tests.csproj", "SecretSanta.Tests/"]
RUN dotnet restore
COPY . .

#testing
FROM build as testing
WORKDIR /src/SecretSanta.Tests
RUN dotnet test

#publish
FROM build AS publish
WORKDIR "/src/SecretSanta"
RUN dotnet publish "SecretSanta.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "SecretSanta.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=https://*:$PORT dotnet SecretSanta.dll