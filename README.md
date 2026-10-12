# Ploch.TemplateConsole

[![Build](https://github.com/mrploch/ploch-templates-dotnet-console/actions/workflows/build.yml/badge.svg)](https://github.com/mrploch/ploch-templates-dotnet-console/actions/workflows/build.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=mrploch_ploch-templates-dotnet-console&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=mrploch_ploch-templates-dotnet-console)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=mrploch_ploch-templates-dotnet-console&metric=coverage)](https://sonarcloud.io/summary/new_code?id=mrploch_ploch-templates-dotnet-console)
[![Licence](https://img.shields.io/github/license/mrploch/ploch-templates-dotnet-console)](LICENSE)

<!-- template-only:start -->
> [!NOTE]
> **This is a GitHub template repository** for a .NET command-line application. Everything between the
> `template-only` markers in this file is removed when a new repository is initialised; the rest becomes the new
> repository's README.

## Using this template

1. Create a repository from this template: **Use this template → Create a new repository** on GitHub, or
   `gh repo create mrploch/<name> --public --template mrploch/ploch-templates-dotnet-console --clone`.
2. Initialise it, then review, build and commit the result:

   ```bash
   pwsh ./scripts/Initialize-Repository.ps1 -Name Ploch.<Product> -WhatIf   # preview
   pwsh ./scripts/Initialize-Repository.ps1 -Name Ploch.<Product>
   dotnet build -c Release && dotnet test -c Release
   git add --all && git commit -m "chore: initialise repository from template"
   ```

   The script renames the solution, projects, namespaces and URLs from `Ploch.TemplateConsole` and
   `ploch-templates-dotnet-console`, removes these template-only sections, then deletes itself and the
   Template Bootstrap workflow that tests it.
3. Complete the [owner setup](#owner-setup) below, then replace the `greet` command with the application's own.

## What the template provides

| Area | What you get |
|---|---|
| Application | A [Spectre.Console.Cli](https://spectreconsole.net/cli/) command-line host with Microsoft.Extensions.DependencyInjection, and [Ploch.Common](https://www.nuget.org/packages/Ploch.Common) from nuget.org |
| Build | .NET SDK pinned in `global.json`, central package management, analysers (.NET, Roslynator, SonarAnalyzer, StyleCop, VS Threading) with warnings as errors in Release |
| Tests | xUnit v3, FluentAssertions, AutoFixture and Spectre.Console.Testing: commands are tested end to end against a `TestConsole` |
| CI | `build.yml` on every push to `main` and every pull request: build, test, coverage, SonarCloud; actions pinned to commit SHAs with least-privilege permissions |
| Coverage | A coverage table in the job summary and one sticky pull-request comment, with the changed lines' coverage and where tests are missing; changed lines must be 80% covered |
| Code quality | SonarCloud analysis with pull-request decoration; the build fails when the new-code quality gate fails |
| Results | A "Test Results" check, published for fork and Dependabot pull requests as well |
| Versioning | Nerdbank.GitVersioning (`version.json`) |
| Hygiene | Dependabot (NuGet, SDK, Actions), issue forms, PR template, CODEOWNERS, CONTRIBUTING, SECURITY, agent instructions |

Every package restores from nuget.org without credentials, so fork and Dependabot pull requests build like any
other. Ploch's command-line library (ploch-commandline) is not used yet because it is published only to GitHub
Packages, which needs a token.

## Keeping the template current

The shared files (workflows, build properties, analyser settings) are maintained canonically in
[mrploch-development](https://github.com/mrploch/mrploch-development) and synced into the template repositories.
Repositories created from the template are independent copies: to adopt a later template change, compare the
file with the template and copy the change.
<!-- template-only:end -->

## Getting started

```bash
dotnet tool restore
dotnet build -c Release
dotnet test -c Release
dotnet run --project src/UI.ConsoleApp -- greet Ada
```

The SDK version is pinned in [`global.json`](global.json). Release builds treat analyser warnings as errors,
exactly as CI does.

## Project layout

| Project | Purpose |
|---|---|
| `src/UI.ConsoleApp` | The executable: `Program.cs`, `ConsoleApplication` (services and commands), `Commands/` |
| `tests/UI.ConsoleApp.Tests` | Unit tests for services, and end-to-end command tests against Spectre's `TestConsole` |

## Continuous integration

| Workflow | Runs on | Does |
|---|---|---|
| [Build](.github/workflows/build.yml) | push to `main`, pull requests, manual | Builds and tests in Release, measures coverage, runs SonarCloud |
| [PR Report](.github/workflows/pr-report.yml) | after every Build run | Publishes the "Test Results" check and the sticky coverage comment |

### Code coverage

Coverage is collected by coverlet in every test project and reported in three places that use the same
exclusions, so they agree:

- **The job summary of every Build run**: overall line and branch coverage, with a per-assembly and per-class table.
- **One comment on each pull request**, updated in place on every push: coverage of the lines the pull request
  changes, listing each file's uncovered lines, followed by the overall summary. Pull requests must cover at
  least **80%** of their changed lines; set the `DIFF_COVERAGE_THRESHOLD` repository variable to change that.
- **SonarCloud**, whose quality gate checks coverage of new code.

The full HTML report is attached to every Build run as the `coverage-report` artefact. Files excluded from
coverage (generated code and migrations) are listed once, in `COVERAGE_EXCLUDE` in `build.yml`.

## Owner setup

These steps need repository or organisation administrator access and cannot be automated from the repository.

### SonarCloud

1. In [SonarCloud](https://sonarcloud.io/projects/create), choose **Analyze new project**, select the `mrploch`
   organisation and this repository. The project key must be `mrploch_ploch-templates-dotnet-console`
   (or set the `SONAR_PROJECT_KEY` repository variable to the key you chose).
2. In the project's **Administration → Analysis Method**, turn **Automatic Analysis off**. CI-based analysis fails
   while it is on.
3. In **Administration → New Code**, select **Previous version**.
4. Provide a token as the `SONAR_TOKEN` secret: an organisation-level secret is inherited by every repository;
   otherwise generate one under **My Account → Security** and add it under **Settings → Secrets and variables →
   Actions**. The organisation defaults to the repository owner; set the `SONAR_ORGANIZATION` variable to override it.

Until `SONAR_TOKEN` exists and the SonarCloud project has been created, the Build workflow skips SonarCloud with
a warning rather than failing, so a new repository is green from its first push. A token that SonarCloud rejects
(HTTP 401 or 403) also skips SonarCloud with a warning, so the build and tests still run; rotate the token to restore
analysis. Any other unexpected SonarCloud response fails the build.

### Security reporting

Under **Settings → Code security**, enable **Private vulnerability reporting**. `SECURITY.md` and the issue-form
contact link send reporters there; with it disabled, they have no private channel.

### Branch protection

Protect `main` and require the **Build, test and analyse** and **Test Results** checks. Do not require the
external **SonarCloud Code Analysis** check: fork and Dependabot pull requests skip SonarCloud, so they would never
report it. The SonarCloud quality gate is enforced inside **Build, test and analyse** instead, which waits for the
gate and fails when it fails.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md). Report security issues privately as described in [SECURITY.md](SECURITY.md).

## Licence

[Apache 2.0](LICENSE)
