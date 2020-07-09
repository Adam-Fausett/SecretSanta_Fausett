#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["SecretSanta/SecretSanta.csproj", "SecretSanta/"]
COPY ["SecretSanta.Core/SecretSanta.Core.csproj", "SecretSanta.Core/"]
COPY ["SecretSanta.Models/SecretSanta.Models.csproj", "SecretSanta.Models/"]
RUN dotnet restore "SecretSanta/SecretSanta.csproj"
COPY . .
WORKDIR "/src/SecretSanta"
RUN dotnet build "SecretSanta.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecretSanta.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "SecretSanta.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SecretSanta.dll