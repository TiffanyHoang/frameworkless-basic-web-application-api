FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY WebApplication ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS run
WORKDIR /app
COPY --from=build /app/out .
ENV COMPlus_EnableDiagnostics=0
CMD ["WebApplication.dll"]
ENTRYPOINT ["dotnet"] 

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS test
WORKDIR /app
COPY . ./
RUN dotnet test