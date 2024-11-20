FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

WORKDIR /app

#
# Note: This section can be removed once we update the builder pattern in Program.cs.
# The CreateDefaultBuilder() method currently doesn't recognize the new ASPNETCORE_HTTP_PORTS environment variable.
# As a result, we need to continue using the old ASPNETCORE_URLS variable until we modify the builder pattern.
#
# Reference: https://learn.microsoft.com/en-us/dotnet/core/compatibility/containers/6.0/aspnet-port
#
ENV ASPNETCORE_URLS "http://*:8080"

EXPOSE 8080

#
# Localization is disabled by default in Alpine images, so we need to enable it manually.
#
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add --no-cache icu-libs

ENV LC_ALL=en_AU.UTF-8
ENV LANG=en_AU.UTF-8

# Copy the pre-built application files into the Docker image
COPY . .

# Run the application using your DLL
CMD ["dotnet", "DNQ.DataFeed.Api.dll"]
