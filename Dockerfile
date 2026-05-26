FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY AuthService.sln ./
COPY src/AuthService.Api/AuthService.Api.csproj src/AuthService.Api/
COPY src/AuthService.Application/AuthService.Application.csproj src/AuthService.Application/
COPY src/AuthService.Domain/AuthService.Domain.csproj src/AuthService.Domain/
COPY src/AuthService.Persistence/AuthService.Persistence.csproj src/AuthService.Persistence/

RUN dotnet restore AuthService.sln

COPY src/ src/
RUN dotnet publish src/AuthService.Api/AuthService.Api.csproj \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN mkdir -p /app/logs /app/keys

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5212
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ENABLE_SWAGGER=true
ENV DISABLE_HTTPS_REDIRECT=true

EXPOSE 5212

ENTRYPOINT ["dotnet", "AuthService.Api.dll"]
