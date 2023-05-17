FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ServerTeledon/ServerTeledon.csproj", "ServerTeledon/"]
RUN dotnet restore "ServerTeledon/ServerTeledon.csproj"
COPY . .
WORKDIR "/src/ServerTeledon"
RUN dotnet build "ServerTeledon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServerTeledon.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerTeledon.dll"]
