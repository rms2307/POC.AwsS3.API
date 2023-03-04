#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["POC.AWS.S3.API/POC.AWS.S3.API.csproj", "POC.AWS.S3.API/"]
RUN dotnet restore "POC.AWS.S3.API/POC.AWS.S3.API.csproj"
COPY . .
WORKDIR "/src/POC.AWS.S3.API"
RUN dotnet build "POC.AWS.S3.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POC.AWS.S3.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POC.AWS.S3.API.dll"]