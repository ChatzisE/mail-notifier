FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY ["app/mail-notifier.csproj", "app/"]
RUN dotnet restore "app/mail-notifier.csproj"
COPY . .
WORKDIR "/app"
RUN dotnet build "app/mail-notifier.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "app/mail-notifier.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mail-notifier.dll"]
