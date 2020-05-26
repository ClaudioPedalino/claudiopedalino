#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["TotvsChallengePoC/TotvsChallengePoC.Api.csproj", "TotvsChallengePoC/"]
COPY ["TotvsChallengePoC.Core/TotvsChallengePoC.Core.csproj", "TotvsChallengePoC.Core/"]
COPY ["TotvsChallengePoC.Data/TotvsChallengePoC.Data.csproj", "TotvsChallengePoC.Data/"]
COPY ["TotvsChallengePoC.Entities/TotvsChallengePoC.Entities.csproj", "TotvsChallengePoC.Entities/"]
COPY ["TotvsChallengePoC.Data.EF/TotvsChallengePoC.Data.EF.csproj", "TotvsChallengePoC.Data.EF/"]
RUN dotnet restore "TotvsChallengePoC/TotvsChallengePoC.Api.csproj"
COPY . .
WORKDIR "/src/TotvsChallengePoC"
RUN dotnet build "TotvsChallengePoC.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TotvsChallengePoC.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TotvsChallengePoC.Api.dll"]