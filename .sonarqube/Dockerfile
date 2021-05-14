FROM mcr.microsoft.com/dotnet/sdk:5.0

ARG HOST_URL="https://sonarqube.svc.platform.myob.com/"
ARG PROJECT_KEY
ARG PROJECT_NAME
ARG TOKEN
ARG SOLUTION_FILE

COPY . ./

# Install Java dependency for SonarScanner and restore SonarScanner
RUN mkdir -p /usr/share/man/man1

RUN apt-get update -y \
    && apt-get install --no-install-recommends -y apt-transport-https \
    && apt-get update -y \
    && apt-get install --no-install-recommends -y openjdk-11-jre \
    && apt-get install -y shellcheck
RUN dotnet tool install --global dotnet-sonarscanner
RUN dotnet tool install --global coverlet.console --version 1.7.2
ENV PATH="$PATH:/root/.dotnet/tools"
ENV LANG C.UTF-8

# Start SonarScanner
RUN dotnet sonarscanner begin \
    /k:"$PROJECT_KEY" \
    /n:"$PROJECT_NAME" \
    /d:sonar.host.url="$HOST_URL" \
    /d:sonar.login="$TOKEN" \
    /d:sonar.cs.opencover.reportsPaths=coverage.opencover.xml


# Run unit tests and build
RUN dotnet build "$SOLUTION_FILE"

RUN  coverlet WebApplication_Tests/bin/Debug/net5.0/WebApplication_Tests.dll \
              --target "dotnet" \
              --targetargs "test WebApplication_Tests/bin/Debug/net5.0/WebApplication_Tests.dll" \
              --format opencover \
              --output "./coverage.opencover.xml"

# Finish SonarScanner
RUN dotnet sonarscanner end \
    /d:sonar.login="$TOKEN"