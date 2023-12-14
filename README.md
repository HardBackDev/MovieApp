# MovieApp
you can browse this project by url: https://moviewatcher.azurewebsites.net
To start the project, first open and run MovieApp.sln, this will create the database and populate all the tables. Then, through Visual Studio Code, open the MovieAppClient folder and enter "npm install" command in the terminal, this will install all the dependencies. After this you can launch the client application and view the application at url "http://localhost:4200/". in app you can login with existing account with username: "Bob" and password: "Password123" to view administrator abilities or login as user with username: "Tucker" and password: "Password123".
# Docker
To start project in docker, in docker-compose.yml file, replace value of ASPNETCORE_Kestrel__Certificates__Default__Password on your certificate password, if you dont know your certificate password, open cmd and write these command one by one 
"dotnet dev-certs https --clean", 
"dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\cert.pfx -p awesomepass", 
"dotnet dev-certs https --trust"
then just open cmd in root folder and write "docker-compose up --build" command. After container will started, you can view app by url "http://localhost:8082/".
# Endpoints  
## MovieCotroller
### (Get) https://localhost:5001/api/movies
#### description:
Gets movies by parameters
#### parameters:
1) SearchedTitle - filters movies by title.
2) SearchedGenres - filters movies by genres, genres should be separated by space, example: "searchedGenres=horror drama"
3) MinDateRelease - defines min date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
4) MaxDateRelease - defines max date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
5) MinGoodRating - defines min good rating for movies.
6) MaxGoodRating - defines max good rating for movies.
7) PageSize - defines page size for response.
8) PageNumber - defines page number for response.
9) OrderBy - defines, by which column will ordered response.
10) OrderDirection - defines direction (descending, ascending) for sorting response.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/movies/withActors
#### description:
Gets movies by parameters, including their actors.
#### parameters:
1) SearchedTitle - filters movies by title.
2) SearchedGenres - filters movies by genres, genres should be separated by space, example: "searchedGenres=horror drama"
3) MinDateRelease - defines min date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
4) MaxDateRelease - defines max date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
5) MinGoodRating - defines min good rating for movies.
6) MaxGoodRating - defines max good rating for movies.
7) PageSize - defines page size for response.
8) PageNumber - defines page number for response.
9) OrderBy - defines, by which column will ordered response.
10) OrderDirection - defines direction (descending, ascending) for sorting response.
11) ActorsCount - defines actors count, includes to response.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/movies/byActor/{actor id}
#### description:
Gets movies by actor id.
#### parameters:
1) SearchedTitle - filters movies by title.
2) SearchedGenres - filters movies by genres, genres should be separated by space, example: "searchedGenres=horror drama"
3) MinDateRelease - defines min date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
4) MaxDateRelease - defines max date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
5) MinGoodRating - defines min good rating for movies.
6) MaxGoodRating - defines max good rating for movies.
7) PageSize - defines page size for response.
8) PageNumber - defines page number for response.
9) OrderBy - defines, by which column will ordered response.
10) OrderDirection - defines direction (descending, ascending) for sorting response.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/movies/byDirector/{director id}
#### description:
Gets movies by director id.
#### parameters:
1) SearchedTitle - filters movies by title.
2) SearchedGenres - filters movies by genres, genres should be separated by space, example: "searchedGenres=horror drama"
3) MinDateRelease - defines min date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
4) MaxDateRelease - defines max date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
5) MinGoodRating - defines min good rating for movies.
6) MaxGoodRating - defines max good rating for movies.
7) PageSize - defines page size for response.
8) PageNumber - defines page number for response.
9) OrderBy - defines, by which column will ordered response.
10) OrderDirection - defines direction (descending, ascending) for sorting response.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/movies/toWatch
#### description:
Gets movies by that user added to watch list.
#### parameters:
1) SearchedTitle - filters movies by title.
2) SearchedGenres - filters movies by genres, genres should be separated by space, example: "searchedGenres=horror drama"
3) MinDateRelease - defines min date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
4) MaxDateRelease - defines max date of release for movies. Date should be types in format "YYYY-MM-DD", example: "minDateRelease=2000-03-22"
5) MinGoodRating - defines min good rating for movies.
6) MaxGoodRating - defines max good rating for movies.
7) PageSize - defines page size for response.
8) PageNumber - defines page number for response.
9) OrderBy - defines, by which column will ordered response.
10) OrderDirection - defines direction (descending, ascending) for sorting response.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/movies/{movie id}
#### description:
Gets movie by id. If the movie is not found, will be returned 404 - not found

### (Get) https://localhost:5001/api/movies/checkInWatching/{movie id}
#### description:
Gets true or false depending on whether this movie in to watching list.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

### (Post) https://localhost:5001/api/movies
#### description:
Creates newly movie.
#### Request Body:
{  
  "title": "",  
  "description": "",  
  "releaseDate": "",  
  "photoUrl": "",  
  "genres": [],  
  "directorId": "",  
  "videoPath": "",  
}
notes: genres should be separated by space.
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Put) https://localhost:5001/api/movies/{movie id}
#### description:
Updates existing movie. If movie not found, will be returned 404 - not found
#### Request Body:
{  
  "title": "",  
  "description": "",  
  "releaseDate": "",  
  "photoUrl": "",  
  "genres": [],  
  "directorId": "",  
  "videoPath": "",  
}  
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Delete) https://localhost:5001/api/movies/{movie id}
#### description:
Deletes movie by id. If movie not found, will be returned 404 - not found
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Post) https://localhost:5001/api/movies/addToWatching/{movie id}
#### description:
Adds movie to watch list.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

### (Delete) https://localhost:5001/api/movies/deleteFromWatching/{movie id}
#### description:
Deletes movie from watch list.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

### (Post) https://localhost:5001/api/movies/addActor/{movie id}/{actor id}
#### description:
Adds relation between actor and movie.
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Post) https://localhost:5001/api/movies/rate/{movie id}/{rate}
#### description:
Rates the movie, for rate should entered "like" or "dislike"
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

### (Delete) https://localhost:5001/api/movies/unrate?id={movie id}
#### description:
Remove your rate from movie by id.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

### (Get) https://localhost:5001/api/movies/getRate/{movie id}
#### description:
Gets rate of movie with specified id.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

## ActorCotroller
### (Get) https://localhost:5001/api/actors
#### description:
Gets actors by parameters
#### parameters:
1) SearchedName - filters actors by name.
2) PageSize - defines page size for response.
3) PageNumber - defines page number for response.
4) OrderBy - defines, by which column will ordered response.
5) OrderDirection - defines direction (descending, ascending) for sorting response.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/actors/{actor id}
#### description:
Gets actor by id, if actor with specified id does not exist, retuning 404 - not found.

### (Get) https://localhost:5001/api/actors/byMovie/{movie id}
#### description:
Gets actors by movie id
#### parameters:
1) SearchedName - filters actors by name.
2) PageSize - defines page size for response.
3) PageNumber - defines page number for response.
4) OrderBy - defines, by which column will ordered response.
5) OrderDirection - defines direction (descending, ascending) for sorting response.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Post) https://localhost:5001/api/actors
#### description:
Creates newly actor.
#### Request Body:
{  
  "name": "",  
  "bio": "",  
  "photoUrl": ""  
}  
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Put) https://localhost:5001/api/actors/{actor id}
#### description:
Updates existing actor. If actor not found, will be returned 404 - not found
#### Request Body:
{  
  "name": "",  
  "bio": "",  
  "photoUrl": ""  
}
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Delete) https://localhost:5001/api/actors/{actor id}
#### description:
Deletes actor by id. If actor not found, will be returned 404 - not found
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

## DirectorController
### (Get) https://localhost:5001/api/directors
#### description:
Gets directors by parameters
#### parameters:
1) SearchedName - filters directors by name.
2) PageSize - defines page size for response.
3) PageNumber - defines page number for response.
4) OrderBy - defines, by which column will ordered response.
5) OrderDirection - defines direction (descending, ascending) for sorting response.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/directors/{director id}
#### description:
Gets director by id, if director with specified id does not exist, retuning 404 - not found.

### (Post) https://localhost:5001/api/directors
#### description:
Creates newly director.
#### Request Body:
{  
  "name": "",  
  "age": 0,  
  "bio": "",  
  "photoUrl": ""  
}  
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Put) https://localhost:5001/api/directors/{director id}
#### description:
Updates existing director. If director not found, will be returned 404 - not found
#### Request Body:
{  
  "name": "",  
  "age": 0,  
  "bio": "",  
  "photoUrl": ""  
}
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Delete) https://localhost:5001/api/directors/{director id}
#### description:
Deletes director by id. If director not found, will be returned 404 - not found
#### Request Headers:
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

## CommentController
### (Get) https://localhost:5001/api/comments/{movie id}
#### description:
Gets comments by parameters.
#### parameters:
1) SearchedText - filters comments by text.
2) PageSize - defines page size for response.
3) PageNumber - defines page number for response.
4) OrderBy - defines, by which column will ordered response.
5) OrderDirection - defines direction (descending, ascending) for sorting response.
#### Response Headers:
1) X-Pagination - includes information about current page, page count, total count, has next, has previous example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Post) https://localhost:5001/api/comments/{movie id}
#### description:
Adds comment to movie
#### Request Body:
comment text.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

### (Put) https://localhost:5001/api/comments/{comment id}
#### description:
Edit comment by id
#### Request Body:
new comment text.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

### (Delete) https://localhost:5001/api/comments/{comment id}
#### description:
Deletes comment.
#### Request Headers:
1) (Required, any role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong, will returning 401 - unauthorized.

## VideoController
### (Post) https://localhost:5001/api/video/upload
#### description:
upload file to wwwroot folder, if file not mp4 extension, returning 400 - bad request
#### Request Body:
file form
1) (Required, Admin role) Authorization - to this header should be entered jwt token. jwt token should be start with prefix "Bearer ". Example: "Bearer {jwt token}". if this header does not exist, or token is wrong or your role is not Admin, will returning 401 - unauthorized.

### (Get) https://localhost:5001/api/video/play?filePath=...
#### description:
returning file stream by file path. If file by path does not exist, returning 404 - not founnd.

## AuthenticationController
### (Post) https://localhost:5001/api/auth
#### description:
registers newly user
#### Request Body:
{
  "userName":"",
  "password":"",
  "email":"",
  "phoneNumber":""
}

### (Post) https://localhost:5001/api/auth/login
#### description:
authenticate user. Returning jwt token if success.
#### Request Body:
{
  "userName":"das",
  "password":"dsad"
}
