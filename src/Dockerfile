FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /mail-notifier

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /mail-notifier
COPY --from=build-env /mail-notifier/out .
ENTRYPOINT ["dotnet", "mail-notifier.dll"]