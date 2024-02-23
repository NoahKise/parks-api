# Oregon State Parks API

#### By Noah Kise

#### A queryable API database of Oregon state parks.

## Technologies Used

* C#
* .NET 6 SDK
* Entity Framework Core
* MySQL
* Token-Based Authentication / Authorization
* Pagination

## Description

This is a web API that holds information about Oregon State Parks. First, a user must register and login using POST API requests. A user can then make API GET requests to query the database, as well as PUT, POST, and DELETE requests to have full CRUD capability.

## Setup/Installation Requirements

* .NET must be installed. Latest version can be found [here](https://dotnet.microsoft.com/en-us/).
* To run locally on your computer, clone the main branch of this [repository](https://github.com/NoahKise/parks-api).
* In your terminal, navigate to the root folder of this project and run `dotnet restore`.
* Open MySQL Workbench. Latest version can be downloaded [here](https://dev.mysql.com/downloads/workbench/).
* Create a new file in the "ParksApi" directory called appsettings.json. NOTE: If you plan to use this project as a jumping off point for further development, you must ensure that appsettings.json is added to your .gitignore file and committed prior to creating this file.
* In `appsettings.json`, enter the following, replacing `USERNAME` and `PASSWORD` to match the settings of your local MySQL server. Replace `DATABASE-NAME` with whatever you would like to name your database.
  
```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=DATABASE-NAME;uid=USERNAME;pwd=PASSWORD;"
    },
    "JWT": {
        "ValidAudience": "example-audience",
        "ValidIssuer": "example-issuer",
        "Secret": "SecretPassword12"
    }
}
```
* In your terminal, navigate to the "ParksApi" directory and run `dotnet ef database update` to create a local database schema seeded with a few parks.
* To view the project in a web browser, navigate to the "ParksApi" directory and run `dotnet watch run`.

## Endpoints

```
GET https://localhost:5001/api/parks/
GET https://localhost:5001/api/parks/{id}
POST https://localhost:5001/api/parks/
PUT https://localhost:5001/api/parks/{id}
DELETE https://localhost:5001/api/parks/{id}
```

 -   A GET request to https://localhost:5001/api/parks will return all park objects.
 -   A GET request to https://localhost:5001/api/parks/{id} will return the park object with the corresponding ParkId property.
 -   A POST request to https://localhost:5001/api/parks will add a new park object to the database. Must contain a request body with the following format:
 ```
    {
    "name": "string",
    "user": "string",
    "imageUrl": "string",
    "camping": true,
    "discGolf": true,
    "kayaking": true,
    "beachAccess": true
  }
  ```
 -   A PUT request to https://localhost:5001/api/parks/{id}?user={userEmail} will edit the park object with the corresponding ParkId property. Must contain a request body with the following format:
 ```
    {
    "parkId": 0,
    "name": "string",
    "user": "string",
    "imageUrl": "string",
    "camping": true,
    "discGolf": true,
    "kayaking": true,
    "beachAccess": true
  }
  ```
  * Note: userEmail in the request url must match the value of "user" in the request body for this request to be successful.
 -   A DELETE request to https://localhost:5001/api/parks/{id}?user={userEmail} will delete the park object with the corresponding ParkId property.
      * Note: userEmail in the request url must match the value of the "User" property contained in the database for the selected park object in order for this request to be successful.

### Optional Query String Parameters for GET Request
| Parameter   | Type        |  Required?    | Description |
| ----------- | ----------- | -----------  | ----------- |
| Name     | String      | not required | Returns parks with name properties containing the query string. |
| Random        | Boolean      | not required | Returns a randomly selected park object. If present in the query, will override any other query parameters. |
| Camping  | boolean      | not required | Returns parks that have camping. |
| DiscGolf  | boolean      | not required | Returns parks that have disc golf. |
| Kayaking  | boolean      | not required | Returns parks that have kayaking. |
| BeachAccess  | boolean      | not required | Returns parks that have beach access. |
| Page  | Integer      | not required | Sets the page number, defaults to 1 |
| PageSize | Integer | not required | Sets the number of results per page, defaults to four |

#### Example Queries

The following query will return all parks with a name property containing "milo":

```
GET https://localhost:5001/api/parks?name=milo
```
You can include multiple query strings by separating them with an `&`:

```
GET https://localhost:5001/api/parks?camping=true&beachAccess=true
```
## License

Code licensed under [GPL](LICENSE.txt)

Any suggestions for ways I can improve this API? Reach out to me at noah@kisefamily.com

Copyright (c) February 23 2024 Noah Kise