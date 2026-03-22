FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QCEServices.Api/QCEServices.Api.csproj", "QCEServices.Api/"]
COPY ["QCEServices.Shared/QCEServices.Shared.csproj", "QCEServices.Shared/"]
COPY ["QCEServices.Infrastructure/QCEServices.Infrastructure.csproj", "QCEServices.Infrastructure/"]
COPY ["QCEServices.Domain/QCEServices.Domain.csproj", "QCEServices.Domain/"]
COPY ["QCEServices.Application/QCEServices.Application.csproj", "QCEServices.Application/"]
RUN dotnet restore "QCEServices.Api/QCEServices.Api.csproj"
COPY . .
WORKDIR "/src/QCEServices.Api"
RUN dotnet build "./QCEServices.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QCEServices.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QCEServices.Api.dll"]
