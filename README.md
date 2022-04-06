# Test Randomizer
The solution **TestRandomizer.sln** contains 2 projects:
 - TestRandomizer - main logic
 - TestRandomizer.UnitTests - unit tests for the components of TestRandomizer project
## Logical Structure
There are several components for achieving the business goal:
 - **Testlet** - hold the collection of items, validates it and creates new randomized sets of these items (depends on **Shuffler**)
 - **ItemTypeEnum** - could be *Operational* or *Pretest*
 - **Item** - contains an identifier and type of an item
 - **Shuffler** - shuffles a set of items in the following way:
	 - Randomly shuffles items (depends on **Randomizer**)
	 - Goes through the new shuffled collection and if first 2 items have *Operational* type, then changes it with the nearest *Pretest* items.
 - **Randomizer** - just a wrapper on *System.Random* for better testability.
## Running the code
To run the project you need to have:
 - .NET 6 runtime installed
 - (optional) Visual Studio 2022

The code has been written in Visual Studio 2022, but it's not neccessary to have it installed. Tests could be executed with the following command:

`dotnet test TestRandomizer.sln`
