
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /home/app
COPY . .
RUN dotnet restore
RUN dotnet publish ./MovieApp/MovieApp.csproj -o /publish/
COPY ./MovieApp/Migrations/create_identity_tables.txt /publish/Migrations/
COPY ./MovieApp/Migrations/create_movies_triggers.txt /publish/Migrations/
WORKDIR /publish
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT ["dotnet", "MovieApp.dll"]

