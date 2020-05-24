# SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
# Copy project files
WORKDIR /source
COPY ["PuppeteerTestApp/PuppeteerTestApp.csproj", "PuppeteerTestApp/"]

# Copy all source code
COPY . .
# Publish
WORKDIR /source
RUN dotnet publish "PuppeteerTestApp/PuppeteerTestApp.csproj" -c Release -o /publish

# Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /publish

#chromium
#RUN apt-get update && apt-get install -yq chromium
RUN apt-get update && \
	apt-get install --no-install-recommends -y chromium && \
    apt-get autoremove && \
    apt-get clean

COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "PuppeteerTestApp.dll"]