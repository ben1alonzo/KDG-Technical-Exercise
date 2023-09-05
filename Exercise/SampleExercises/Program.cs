// imports
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleDataManagement.Models;
using System.Collections.Generic;
using System.Text.Json.Nodes;


var dataSourcesDirectory = Path.Combine(Environment.CurrentDirectory, "DataSources");
var personsFilePath = Path.Combine(dataSourcesDirectory, "Persons_20220824_00.json");
var organizationsFilePath = Path.Combine(dataSourcesDirectory, "Organizations_20220824_00.json");
var vehiclesFilePath = Path.Combine(dataSourcesDirectory, "Vehicles_20220824_00.json");
var addressesFilePath = Path.Combine(dataSourcesDirectory, "Addresses_20220824_00.json");

//Quick test to ensure that all files are where they should be :)
foreach (var path in new[] { personsFilePath, organizationsFilePath, vehiclesFilePath, addressesFilePath })
{
    if (!File.Exists(path))
        throw new FileNotFoundException(path);
}

// TODO: Start your exercise here. Do not forget about answering Test #9 (Handled slightly different)
// Reminder: Collect the data from each file. Hint: You could use Newtonsoft's JsonConvert or Microsoft's JsonSerializer

// ---------------------------------------------------------------------------------------------------------------

// Load and parse JSON data for all four entities using JArray
string persons = File.ReadAllText(personsFilePath);
JArray personsArray = JArray.Parse(persons);

string organizations = File.ReadAllText(organizationsFilePath);
JArray organizationsArray = JArray.Parse(organizations);

string vehicles = File.ReadAllText(vehiclesFilePath);
JArray vehiclesArray = JArray.Parse(vehicles);

string addresses = File.ReadAllText(addressesFilePath);
JArray addressesArray = JArray.Parse(addresses);

// Check each parsed dataset for null case
if (personsArray == null) {
    throw new NotImplementedException("An error was encountered while parsing persons entities.");
}
if (organizationsArray == null) {
    throw new NotImplementedException("An error was encountered while parsing organizations entities.");
}
if (vehiclesArray == null) {
    throw new NotImplementedException("An error was encountered while parsing vehicles entities.");
}
if (addressesArray == null) {
    throw new NotImplementedException("An error was encountered while parsing addresses entities.");
}

// ----------------------------- QUESTION 1 -------------------------------------------------------------------------

// Test #1: Do all files have entities? (True / False)
// Check if each entity type is present in the respective JSON files

// Create boolean representation to check each dataset for empty JSON
bool personsPresent = personsArray.Count > 0;
bool organizationsPresent = organizationsArray.Count > 0;
bool vehiclesPresent = vehiclesArray.Count > 0;
bool addressesPresent = addressesArray.Count > 0;

// Boolean variable assigned true if all datasets are adequately parsed and contain objects
bool allEntitiesPresent = personsPresent && organizationsPresent && vehiclesPresent && addressesPresent;

// Write answer to console: TRUE if all entities are present, FALSE if not
Console.WriteLine($"Q1:\n{allEntitiesPresent.ToString().ToUpper()}\n");

// ----------------------------- QUESTION 2 -------------------------------------------------------------------------

// Test #2: What is the total count for all entities?

int totalPersons = personsArray.Count;
int totalOrganizations = organizationsArray.Count;
int totalVehicles = vehiclesArray.Count;
int totalAddresses = addressesArray.Count;

int totalEntityCount = totalPersons + totalOrganizations + totalVehicles + totalAddresses;

// Write answer to console, providing a count of total entities
Console.WriteLine($"Q2:\n{totalEntityCount}\n");

// ----------------------------- QUESTION 3 -------------------------------------------------------------------------

// Test #3: What is the count for each type of Entity? Person, Organization, Vehicle, and Address

// Reusing variables created in Test #2, write entity counts for each type of entity to console.
Console.WriteLine($"Q3:\n" +
    $"Person: {totalPersons}\n" +
    $"Organization: {totalOrganizations}\n" +
    $"Vehicle: {totalVehicles}\n" +
    $"Address: {totalAddresses}\n");

// ----------------------------- QUESTION 4 -------------------------------------------------------------------------

// Test #4: Provide a breakdown of entities which have associations in the following manner:
//         - Per Entity Count
//         - Total Count

// Retrieve counts of each entity type using LINQ within a lambda expression
int personAssociations = personsArray.Count(j => j["Associations"].Count() != 0);
int organizationAssociations = organizationsArray.Count(j => j["Associations"].Count() != 0);
int vehicleAssociations = vehiclesArray.Count(j => j["Associations"].Count() != 0);
int addressAssociations = addressesArray.Count(j => j["Associations"].Count() != 0);

int totalAssociations = personAssociations + organizationAssociations + vehicleAssociations + addressAssociations;

// Write answer to console, formatted to include both per-entity counts and a total count
Console.WriteLine("Q4:\n" +
    "- Per Entity Count:\n" +
    $"Person: {personAssociations}\n" +
    $"Organization: {organizationAssociations}\n" +
    $"Vehicle: {vehicleAssociations}\n" +
    $"Address: {addressAssociations}\n" +
    $"- Total Count: {totalAssociations}\n");

// ----------------------------- QUESTION 5 -------------------------------------------------------------------------

// Test #5: Provide the vehicle detail that is associated to the address "4976 Penelope Via South Franztown, NH 71024"?
//         StreetAddress: "4976 Penelope Via"
//         City: "South Franztown"
//         State: "NH"
//         ZipCode: "71024"

// Define address constants
string targetStreetAddress = "4976 Penelope Via";
string targetCity = "South Franztown";
string targetState = "NH";
string targetZipCode = "71024";

// Query the Associations from the specified Address within the Address JArray 
var associationsArray = addressesArray
    .Where(a =>
        string.Equals(a["StreetAddress"].ToString(), targetStreetAddress) &&
        string.Equals(a["City"].ToString(), targetCity) &&
        string.Equals(a["State"].ToString(), targetState) &&
        string.Equals(a["ZipCode"].ToString(), targetZipCode))
    .Select(address => address["Associations"])
    .FirstOrDefault();

// Extract the Vehicle EntityID from the Associations
string associatedEntityId = associationsArray
    .FirstOrDefault()?["EntityId"]?.ToString();

// Query the Vehicle Associated with the specified Address from the Vehicles JArray
var associatedVehicle = vehiclesArray
    .Where(vehicle => 
        string.Equals(vehicle["EntityId"].ToString(),
        associatedEntityId,
        StringComparison.OrdinalIgnoreCase))
    .FirstOrDefault();

// Write answer to console, formatted to include all fields specified within the answer key
Console.WriteLine($"Q5:\n" +
    $"Id: {associatedVehicle["EntityId"]}\n" +
    $"Make: {associatedVehicle["Make"]}\n" +
    $"Model: {associatedVehicle["Model"]}\n" +
    $"Year: {associatedVehicle["Year"]}\n" +
    $"Plate: {associatedVehicle["PlateNumber"]}\n" +
    $"State: {associatedVehicle["State"]}\n" +
    $"Vin: {associatedVehicle["Vin"]}\n");

// ----------------------------- QUESTION 6 -------------------------------------------------------------------------

// Test #6: What person(s) are associated to the organization "thiel and sons"?

// Define organization name
string thielAndSons = "thiel and sons";

// Query the "thiel and sons" EntityId
string thielAndSonsEntityId = organizationsArray
    .Where(org => 
        string.Equals(org["EntityId"].ToString(),
        thielAndSons,
        StringComparison.OrdinalIgnoreCase))
    .Select(org => org["EntityId"].ToString())
    .FirstOrDefault();

// Query a List of Persons Associated with "thiel and sons"
var associatedPersons = personsArray
    .Where(person => person["Associations"]
    .Any(assoc => assoc["EntityId"].ToString() == thielAndSonsEntityId))
    .ToList();

Console.WriteLine("Q6:");

// Write to console all Persons Associated with "thiel and sons", or
// "None" if no such Associations exist
if (associatedPersons.Count() == 0) { 
    Console.WriteLine("None\n");
}
else {
    foreach (var person in associatedPersons) {
        Console.WriteLine($"{person}\n");
    }
}

// ----------------------------- QUESTION 7 -------------------------------------------------------------------------

// Test #7: How many people have the same first and middle name?

// Create a count of a query of Person that meet the condition FirstName == MiddleName
var sameFirstMiddleCount = personsArray
    .Count(person => 
        string.Equals(person["FirstName"].ToString(), 
        person["MiddleName"].ToString(), 
        StringComparison.OrdinalIgnoreCase));

// Write the count to console
Console.WriteLine($"Q7:\n{sameFirstMiddleCount}\n");

// ----------------------------- QUESTION 8 -------------------------------------------------------------------------

// Test #8: Provide a breakdown of entities where the EntityId contains "B3" in the following manor:
//         - Total count by type of Entity
//         - Total count of all entities

// Takes a JArray and string input and performs a case-insensitive search within the given Array
// for EntityId's that contain the given string. Returns the total number of matches.
static int searchArrayEntities(JArray array, string searchQuery)
{
    return array
        .Count(person => person["EntityId"].ToString()
            .Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
}

// Call the refactored searchArrayEntities method to count the number of entities that
// contain "B3" in each array.
int personsCount = searchArrayEntities(personsArray, "B3");
int vehiclesCount = searchArrayEntities(vehiclesArray, "B3");
int organizationsCount = searchArrayEntities(organizationsArray, "B3");
int addressesCount = searchArrayEntities(addressesArray, "B3");
int totalCount = personsCount + vehiclesCount + organizationsCount + addressesCount;

// Write answer to console, formatted to include both per-entity counts and a total count
Console.WriteLine("Q8:\n" +
    "- Per Entity Count:\n" +
    $"Person: {personsCount}\n" +
    $"Vehicle: {vehiclesCount}\n" +
    $"Organization: { organizationsCount}\n" +
    $"Address: {addressesCount}\n" +
    $"- Total Count: {totalCount}");


