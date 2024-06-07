#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Payment/Payment.GRPC/Payment.GRPC.csproj", "Services/Payment/Payment.GRPC/"]
COPY ["Services/Payment/Payment.Common/Payment.Common.csproj", "Services/Payment/Payment.Common/"]
RUN dotnet restore "./Services/Payment/Payment.GRPC/Payment.GRPC.csproj"
COPY . .
WORKDIR "/src/Services/Payment/Payment.GRPC"
RUN dotnet build "./Payment.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Payment.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.GRPC.dll"]
