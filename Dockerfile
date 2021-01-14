
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["UmHelpFinanceiro/UmHelpFinanceiro.csproj", "UmHelpFinanceiro/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "UmHelpFinanceiro/UmHelpFinanceiro.csproj"
COPY . .
WORKDIR "/src/UmHelpFinanceiro"
RUN dotnet build "UmHelpFinanceiro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UmHelpFinanceiro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UmHelpFinanceiro.dll"]