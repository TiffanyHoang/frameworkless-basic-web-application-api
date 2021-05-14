FROM mcr.microsoft.com/dotnet/core/sdk:3.1

ARG HOST_URL="https://sonarqube.svc.platform.myob.com/"
ARG PROJECT_KEY
ARG PROJECT_NAME
ARG TOKEN
ARG SOLUTION_FILE

COPY . ./

# Install Java dependency for SonarScanner and restore SonarScanner
RUN apt-get update && apt-get install -y default-jre
RUN dotnet tool install --global dotnet-sonarscanner
ENV PATH="$PATH:/root/.dotnet/tools"
ENV LANG C.UTF-8

# Start SonarScanner
RUN dotnet sonarscanner begin \
    /k:"$PROJECT_KEY" \
    /n:"$PROJECT_NAME" \
    /d:sonar.host.url="$HOST_URL" \
    /d:sonar.login="$TOKEN"

# Run unit tests and build
RUN dotnet build "$SOLUTION_FILE"

# Finish SonarScanner
RUN dotnet sonarscanner end \
    /d:sonar.login="$TOKEN"