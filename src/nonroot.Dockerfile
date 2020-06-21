FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
EXPOSE 5000
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY ["./mail-notifier/src/app/mail-notifier.csproj", "app/"]
RUN dotnet restore "app/mail-notifier.csproj"
COPY . .
RUN dotnet build "./mail-notifier/src/app/mail-notifier.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./mail-notifier/src/app/mail-notifier.csproj" -c Release -o /app/publish

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mail-notifier.dll"]
