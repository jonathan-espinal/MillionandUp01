#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MillionandUpBackend01/MillionandUpBackend01.csproj", "MillionandUpBackend01/"]
RUN dotnet restore "MillionandUpBackend01/MillionandUpBackend01.csproj"
COPY . .
WORKDIR "/src/MillionandUpBackend01"
RUN dotnet build "MillionandUpBackend01.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MillionandUpBackend01.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MillionandUpBackend01.dll"]