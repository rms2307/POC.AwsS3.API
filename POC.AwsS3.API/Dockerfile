#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["POC.AwsS3.API/POC.AwsS3.API.csproj", "POC.AwsS3.API/"]
RUN dotnet restore "POC.AwsS3.API/POC.AwsS3.API.csproj"
COPY . .
WORKDIR "/src/POC.AwsS3.API"
RUN dotnet build "POC.AwsS3.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POC.AwsS3.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POC.AwsS3.API.dll"]