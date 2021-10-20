# BuildingManager
BuildingManager is the back-end implementation of building list management.

## Project information
Project type: ASP.NET;\
Target Framework: .NET 5.0;\
ORM: Entity Framework Core / SQL server;\
Test: xUnit.

## Service end-points
- GET /api/buildings - get all building names
- GET /api/buildings/id - get detailed building info
- GET /search?filter - get detailed building list for a given search criteria
- POST /api/buildings - create new building info
- PATCH /api/buildings/id - update existing building info

- GET /api/buildingowners - get all building owner names
- GET /api/buildingowners/id - get detailed building owner info
- POST /api/buildingowners - create new building owner info
- PATCH /api/buildingowners/id - update existing building owner info

## Database creation and seeding
SQL script to create and seed the database is located in:
https://github.com/ilyadubovis/BuildingManager/blob/master/Data/SQLDeployment/test.sql

