FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Teledon/Teledon.csproj", "Teledon/"]
RUN dotnet restore "Teledon/Teledon.csproj"
COPY . .
WORKDIR "/src/Teledon"
RUN dotnet build "Teledon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teledon.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teledon.dll"]
