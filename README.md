# .NET GitHub Debugging Challenge — Find the Bugs 🔍

**Tech No Bla Bla | Senior .NET Repository Investigation (C# / .NET 10 / xUnit)**

This repository is **intentionally broken**. It simulates a real interview/take-home debugging assignment: clone unfamiliar code, inspect the repo, understand business rules, run tests, diagnose failures and implement safe fixes.

> **Warning:** Do not use this code in production. The bugs are deliberate educational examples.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (10.0.100 or compatible later feature band)
- Git, VS Code + C# Dev Kit (optional), or Visual Studio
- Internet for the initial NuGet restore of xUnit packages

## Quick start

```bash
git clone https://github.com/YOUR-USERNAME/dotnet-repo-debugging-challenge.git
cd dotnet-repo-debugging-challenge
dotnet --info
dotnet restore
dotnet build --no-restore
dotnet test --no-restore
dotnet run --project src/OrderChallenge.App
```

Replace `YOUR-USERNAME` with the real GitHub account name after publishing.

## First 15 minutes: investigate, don't randomly edit

1. Open `README.md`, then `docs/BUSINESS_RULES.md`.
2. Discover projects in `src/` and `tests/`, then inspect `.csproj` files.
3. Search for `Quote`, `TryReserve`, and failing test names.
4. Run all tests once; note the **expected vs actual** failure messages.
5. Identify the smallest reproduction and trace input → calculation → output.
6. Fix one root cause at a time, rerunning the relevant tests.
7. Rerun the entire suite, review `git diff`, then commit changes on a branch.

## Repository map

```text
src/
  OrderChallenge.Core/
    Models/                 # small domain types
    Services/               # implementation under investigation
  OrderChallenge.App/       # demo console application
tests/
  OrderChallenge.Tests/     # executable specifications (xUnit)
docs/
  BUSINESS_RULES.md         # authoritative product behavior
.github/workflows/
  dotnet.yml                # build and test in CI
```

## Useful debugging commands

```bash
dotnet test --filter FullyQualifiedName~Subtotal
dotnet test --filter FullyQualifiedName~Failed_Reservation
dotnet test --logger "console;verbosity=normal"
git status
git diff
git log --oneline -5
```

## Your challenge

Fix the broken behavior in `OrderCalculator` and `InventoryService` while honoring every business requirement. Explain why the initial implementation failed and how your change avoids regressions.

**No solution is published on the default branch.** Watch the Tech No Bla Bla walkthrough after you attempt it!

## Suggested workflow for a take-home exercise

- Create `fix/checkout-and-inventory` from `main`.
- Use tests as evidence; never edit a test merely to conceal a failure.
- Commit small, reviewable changes, push the branch, open a pull request.

License: MIT.
