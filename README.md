# MovieApp
To start the project, first open and run MovieApp.sln, this will create the database and populate all the tables. Then, through Visual Studio Code, open the MovieAppClient folder and enter "npm install" command in the terminal, this will install all the dependencies. After this you can launch the client application and view the application at url "http://localhost:4200/". in app you can login with existing account with username: "Bob" and password: "Password123" to view administrator abilities or login as user with username: "Tucker" and password: "Password123".
# Docker
To start project in docker, in docker-compose.yml file, replace value of ASPNETCORE_Kestrel__Certificates__Default__Password on your certificate password, if you dont know your certificate password, open cmd and write these command one by one 
"dotnet dev-certs https --clean", 
"dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\cert.pfx -p awesomepass", 
"dotnet dev-certs https --trust"
then just open cmd in root folder and write "docker-compose up --build" command. After container will started, you can view app by url "http://localhost:8082/".
