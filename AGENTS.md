# Agent instructions

Instructions for AI coding agents working in this repository. Human contributors: see
[CONTRIBUTING.md](CONTRIBUTING.md).

## Repository

- .NET command-line application, `Ploch.TemplateConsole`, targeting the SDK pinned in `global.json`.
- `src/UI.ConsoleApp` is the executable (Spectre.Console.Cli with Microsoft.Extensions.DependencyInjection);
  `tests/` holds its tests (xUnit v3, FluentAssertions, AutoFixture, Spectre.Console.Testing).
- Package versions are pinned centrally in `Directory.Packages.props`; never put a version in a `.csproj`.
- Shared MSBuild settings live in `Directory.Build.props`. Versions come from `version.json` (Nerdbank.GitVersioning).

## Commands

```bash
dotnet tool restore
dotnet build Ploch.TemplateConsole.slnx -c Release
dotnet test Ploch.TemplateConsole.slnx -c Release
dotnet run --project src/UI.ConsoleApp -- --help
```

## Rules

- Register new commands in `ConsoleApplication.Create`; inject `IAnsiConsole` instead of using the static
  `AnsiConsole`, so commands stay testable against `TestConsole`.
- Release builds treat warnings as errors. Fix analyser warnings; do not suppress them without a written reason.
- Test methods are named `<Member>_should_<expected_behaviour>`; test classes `<TypeUnderTest>Tests`.
- New and changed code needs tests: CI requires 80% coverage of changed lines, and SonarCloud gates new code.
- Commits follow Conventional Commits. Add a `change-log/` entry for every user-visible change.
- Never commit credentials. Secrets reach CI only through GitHub secrets; see `README.md`.
