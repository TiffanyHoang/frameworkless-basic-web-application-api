FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /App
COPY . .
EXPOSE 8080

ENTRYPOINT [ "dotnet", "run", "--project", "WebApplication" ]
