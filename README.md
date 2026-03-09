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
│   │   ├── IFsaClient.cs                    — FSA client interface
│   │   └── FsaClient.cs                     — HTTP client for the FSA API
│   ├── Model/
│   │   ├── Authority.cs
│   │   ├── AuthorityRatingItem.cs
│   │   ├── FsaAuthority.cs
│   │   └── FsaAuthorityList.cs
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
    ├── ApiScenarioContext.cs                — Shared scenario state (context injection)
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

## References

| Library | Description | Documentation |
|---------|-------------|---------------|
| [Reqnroll](https://reqnroll.net) | BDD test framework for .NET (successor to SpecFlow) | [docs.reqnroll.net](https://docs.reqnroll.net) |
| [Reqnroll + NUnit](https://docs.reqnroll.net/latest/installation/setup-reqnroll-project.html) | Setting up Reqnroll with NUnit | [Installation guide](https://docs.reqnroll.net/latest/installation/setup-reqnroll-project.html) |
| [NUnit](https://nunit.org) | Unit testing framework for .NET | [docs.nunit.org](https://docs.nunit.org) |
