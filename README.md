# SecretSanta_Fausett
Secret Santa!
This is a simple web application for choosing names for a Secret Santa gift exchange.
The applicatin provides support for both individual participants as well as the ability to define groups of participants that should be prevented from selecting each other (i.e., prevent family members from selecting fellow family members, etc.)

There is a web UI and also a REST api as part of this project.

## Prerequisites
- .Net Framework 4.7

## Installation/Run Instructions
- Clone the repository
- Open the solution in Visual Studio
- Restore NuGet packages
  - right-click on the solution > restore NuGet packages...
- Run locally using IIS Express
  - this should open a browser for you automatically

## Navigating the app
Using the site is pretty straight-forward. There is a section for listing the `Individual Participants` involved. 
All participants listed in this section are fair game to be able to draw one-another etc. 

`Groups` are supported -- a group is defined as a set of participants that should not be able to draw one-another (members of the same family perhaps).
To add a group, click the "Create Group" button. A new group section will appear for the participants in that group.
There is no limit on the number of groups or participants in the groups.

When ready to select names for the participants, click the "Draw Names" button. The re-drawing of names is also supported if the outcome is not desirable. 

## REST API
- `HttpPost`
- http://.../api/SecretSanta/DrawNames

The request object for this endpoint is a list of lists of `Participant` objects.
- perhaps easier to think about as a list of groups of participants
  - individual participants are represented as a group of 1

public view of the Participant class:
```C#
public class Participant 
{
    public string Name { get; set; }
}
```

#### Example Request
```json
[
  [ { "Name": "Santa" }, { "Name": "Mrs. Claus" } ],
  [ { "Name": "Rudolph" } ],
  [ { "Name": "Cornelius" } ],
  [ { "Name": "Dasher" }, { "Name": "Prancer" }, { "Name": "Donner" }, { "Name": "Vixen" } ]
]
```
The response is a list of `SecretSanta` objects.

public view of the SecretSanta class:
```C#
public class SecretSanta : Participant 
{
    public Participant Recipient { get; set; }
}
```

#### Example Response
```json
[
  { "Name": "Dasher", "Recipient": { "Name": "Cornelius" } },
  { "Name": "Donner", "Recipient": { "Name": "Rudolph" } },
  { "Name": "Prancer", "Recipient": { "Name": "Mrs. Claus" } }
  { "Name": "Vixen", "Recipient": { "Name": "Santa" } }
  { "Name": "Santa", "Recipient": { "Name": "Donner" } }
  { "Name": "Mrs. Claus", "Recipient": { "Name": "Prancer" } }
  { "Name": "Rudolph", "Recipient": { "Name": "Dasher" } }
  { "Name": "Cornelius", "Recipient": { "Name": "Vixen" } }
]
```

## Testing
There also exists a small suite of Unit Tests around the core section of drawing names from the given participants.
- these tests can also be ran within Visual Studio
