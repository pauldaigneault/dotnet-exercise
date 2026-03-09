# Full Stack Tech Test: .NET

![Preview of Frontend](preview.png)

A .NET 8 Web API that wraps the UK Food Standards Agency (FSA) public food hygiene ratings API, with a lightweight frontend served as static files.

## Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- Internet access (the API calls the live FSA service)
- A suitable development environment (e.g. VS Code, JetBrains Rider, Visual Studio)

## Project Structure

```
dotnet-exercise/
├── FoodRatingApp.sln                         — Solution file
│
├── FoodRatingApp/                            — Main Web API
│   ├── Controllers/
│   │   └── RatingController.cs              — GET /api and GET /api/{id}
│   ├── Services/
│   │   ├── IFSAClient.cs                    — FSA client interface
│   │   └── FSAClient.cs                     — HTTP client for the FSA API
│   ├── Model/
│   │   ├── Authority.cs
│   │   ├── AuthorityRatingItem.cs
│   │   ├── FSAAuthority.cs
│   │   └── FSAAuthorityList.cs
│   ├── wwwroot/                             — Static frontend (HTML + CSS)
│   ├── Program.cs
│   └── FoodRatingApp.csproj
│
├── FoodRatingApp.Test/                       — NUnit unit tests
│   ├── Controllers/
│   │   └── RatingControllerTests.cs
│   └── FoodRatingApp.Test.csproj
│
└── FoodRatingApp.Spec/                       — Reqnroll BDD specs
    ├── Features/
    │   ├── FoodRatings.feature              — Passing example
    │   ├── AuthorityList.feature            — Candidate task
    │   └── AuthorityRatings.feature         — Candidate task
    ├── StepDefinitions/
    │   ├── FoodRatings.cs                   — Example step definitions
    │   └── ContractStepDefinitions.cs       — Skeleton steps to implement
    └── FoodRatingApp.Spec.csproj
```

## Getting Started

### Restore dependencies

```bash
dotnet restore
```

### Build the solution

```bash
dotnet build FoodRatingApp.sln
```

### Run the API

```bash
dotnet run --project ./FoodRatingApp/FoodRatingApp.csproj
```

Once running:

| URL | Description |
|-----|-------------|
| `https://localhost:5001` | Frontend UI |
| `https://localhost:5001/api` | List of authorities (JSON) |
| `https://localhost:5001/api/{id}` | Ratings for a specific authority (JSON) |

## Running Tests

### Run all tests

```bash
dotnet test FoodRatingApp.sln
```

### Run unit tests only

```bash
dotnet test FoodRatingApp.Test/FoodRatingApp.Test.csproj
```

### Run BDD specs only

```bash
dotnet test FoodRatingApp.Spec/FoodRatingApp.Spec.csproj
```

### Run a specific feature file

```bash
dotnet test FoodRatingApp.Spec/FoodRatingApp.Spec.csproj --filter "FullyQualifiedName~FoodRatings"
dotnet test FoodRatingApp.Spec/FoodRatingApp.Spec.csproj --filter "FullyQualifiedName~AuthorityList"
dotnet test FoodRatingApp.Spec/FoodRatingApp.Spec.csproj --filter "FullyQualifiedName~AuthorityRatings"
```
