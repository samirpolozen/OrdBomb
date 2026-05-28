FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /src

COPY OrdBomb.Api/*.csproj ./OrdBomb.Api/
RUN cd OrdBomb.Api && dotnet restore

COPY OrdBomb.Api/ ./OrdBomb.Api/

RUN cd OrdBomb.Api && dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=builder /app/publish .

CMD ["sh", "-c", "ASPNETCORE_URLS=http://+:${PORT:-10000} dotnet OrdBomb.Api.dll"]