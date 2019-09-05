# SWAPIStop.NET

This is a code challenge project that for given input needs to determine total amount
of stops required to make the distance between the planets for each starship in the API.

This project is written in C# (.NET Core 2.2 console application).

## SWAPI

Data for this project is used from [SWAPI](https://swapi.co/).
By the time of doing this project, SWAPI have a collection of 37 starships.
Those records are delivered through 4 pages in the REST API.

API is called with next example:

```rest
https://swapi.co/api/starships/?page=1
```

Response data is structured like this:

- "count": number of items in the collection
- "next": url to the next page
- "previous": url to the previous page
- "results": array of JSON objects:
    - "name": name of the starship
    - "model": starship model
    - "manufacturer": who manufactured this starship
    - "cost_in_credits": starship price
    - "max_atmosphering_speed": maximum starship speed
    - "crew": number of personal on the starship
    - "passengers": number of passengers
    - "cargo_capacity": capacity of cargo in unknown measuring unit
    - "consumables": number of consumables
    - "hyperdrive_rating": rating of the hyperdrive
    - "MGLT": stands for "mega light", which is a standard unit of distance in space (in Star Wars). It is often used in sublight speed measurement (example: 100 MGLT per hour)
    - "starship_class": type of starship
    - "pilots": list of url that represent pilot details
    - "films": list of url that represent movie details in which this starship have appeared
    - "created": creation time
    - "edited": editing time
    - "url": url to this starship (last parameter of url is starship id)

## Project details

As mentioned, project is written in C# (.NET Core console application).

Project is structured like this:

- Project
    - Constants
        - PeriodEnumeration.cs
        - Url.cs
    - Data
        - StoppingHandler.cs
    - Models
        - Starship.cs
        - SWAPIJson.cs
    - Utilities
        - SWAPIClient.cs
    - Program.cs

Project have 3 dependencies:

1) Microsoft.Extensions.DependencyInjection
2) Microsoft.Extensions.Http
3) Newtonsoft.Json

There is also Test project "SWAPIStop.Test" which contains only 3 tests:

1) SWAPI must return 37 items (in the time of doing this project, SWAPI have only 37 starships)
2) Testing converting of consumables to its valid representation in days
3) Testing that calculation give valid number of stops for some starship (in the test sample, I was using Millenium Falcon)

## Running application

To run application, first restore all NuGet packages and then run the project.

Application will only ask for a input (will give error message if input is not valid integer).

After that, application will fetch the data from the SWAPI and do calculations.
There is also a tracker in the code which is used to display how many starships in the collection can't be a part of calculation because of non valid MGLT or non valid consumables.

Besides that, application will display name of each starship and how many stop that starship needs to stop for resupply in order to cover a given distance.
