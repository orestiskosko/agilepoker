FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /source

COPY *.sln .
COPY Sapp.Common/ ./Sapp.Common/
COPY Sapp.Core/ ./Sapp.Core/
COPY Sapp.Api/ ./Sapp.Api/
RUN dotnet restore

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build /app ./
# ENTRY POINT FOR HEROKU
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Sapp.Api.dll
#ENTRYPOINT ["dotnet", "Sapp.Api.dll"]