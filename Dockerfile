FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /build
COPY Mublog.Server.sln ./
COPY src/PublicApi/*.csproj src/PublicApi/
COPY src/Infrastructure/*.csproj src/Infrastructure/
COPY src/Application/*.csproj src/Application/
COPY src/Domain/*.csproj src/Domain/

RUN dotnet restore "src/PublicApi/PublicApi.csproj"

COPY . .

WORKDIR /build/src/PublicApi
RUN dotnet build -c Release -o /app

WORKDIR /build/src/Infrastructure
RUN dotnet build -c Release -o /app

WORKDIR /build/src/Application
RUN dotnet build -c Release -o /app

WORKDIR /build/src/Domain
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Mublog.Server.PublicApi.dll"]