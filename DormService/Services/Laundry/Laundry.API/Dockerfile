FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Laundry/Laundry.API/Laundry.csproj", "Services/Laundry/Laundry.API/"]
RUN dotnet restore "Services/Laundry/Laundry.API/Laundry.csproj"
COPY . .
WORKDIR "/src/Services/Laundry/Laundry.API"
RUN dotnet build "Laundry.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Laundry.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Laundry.dll"]
