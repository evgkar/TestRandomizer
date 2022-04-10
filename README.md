# Test Randomizer
The solution **TestRandomizer.sln** contains 2 projects:
 - TestRandomizer - main logic
 - TestRandomizer.UnitTests - unit tests for the components of TestRandomizer project
## Logical Structure
There are several components for achieving the business goal:
 - **Testlet** - hold the collection of items, validates it and creates new randomized sets of these items (depends on **Randomizer**) using the following logic:
   - Shuffles Pretest items
   - Takes first 2 Pretest items
   - Shuffles rest (Pretest + Operational) items
 - **ItemType** - could be *Operational* or *Pretest*
 - **Item** - contains an identifier and type of an item
 - **Randomizer** - just a wrapper on *System.Random* for better testability.
## Running the code
To run the project you need to have:
 - .NET 6 runtime installed
 - (optional) Visual Studio 2022

The code has been written in Visual Studio 2022, but it's not neccessary to have it installed. Tests could be executed with the following command:

`dotnet test TestRandomizer.sln`
