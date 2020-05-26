#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["TovusChallenge.Api.csproj", ""]
COPY ["../TotvsChallenge.DataAccess/TotvsChallenge.DataAccess.csproj", "../TotvsChallenge.DataAccess/"]
COPY ["../TotvsChallenge.Data/TotvsChallenge.Data.csproj", "../TotvsChallenge.Data/"]
COPY ["../TotvsChallenge.Entities/TotvsChallenge.Entities.csproj", "../TotvsChallenge.Entities/"]
COPY ["../TotvsChallenge.Core/TotvsChallenge.Core.csproj", "../TotvsChallenge.Core/"]
RUN dotnet restore "./TovusChallenge.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TovusChallenge.Api.csproj" -c Release -o ./app

FROM build AS publish
RUN dotnet publish "TovusChallenge.Api.csproj" -c Release -o ./app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TovusChallenge.Api.dll"]