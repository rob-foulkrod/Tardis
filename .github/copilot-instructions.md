# Tardis - .NET 9 ASP.NET Core MVC Web Application

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### Environment Setup
- Install .NET 9 if not available:
  - `wget https://dot.net/v1/dotnet-install.sh && chmod +x dotnet-install.sh`
  - `./dotnet-install.sh --version 9.0.100 --install-dir ~/.dotnet`
  - `export PATH="$HOME/.dotnet:$PATH"`
- Verify .NET version: `dotnet --version` (should show 9.0.100 or higher)

### Build and Test Commands
Always run these commands from the `src/` directory:

- **Restore dependencies**: `dotnet restore Tardis.sln` -- takes 10 seconds. NEVER CANCEL. Set timeout to 60+ seconds.
- **Build (Development)**: `dotnet build Tardis.sln --configuration Debug` -- takes 12 seconds. NEVER CANCEL. Set timeout to 60+ seconds.
- **Build (Release)**: `dotnet build Tardis.sln --configuration Release` -- takes 12 seconds. NEVER CANCEL. Set timeout to 60+ seconds.
- **Build with linting**: `dotnet build Tardis.sln --no-restore -warnaserror` -- treats warnings as errors for StyleCop analysis. Takes 2 seconds after restore.
- **Run tests**: `dotnet test Tardis.sln --no-build` -- takes 2 seconds. NEVER CANCEL. Set timeout to 30+ seconds.
- **Test with coverage**: 
  ```bash
  dotnet test Tardis.sln --configuration Release --logger "trx;LogFileName=tests.trx" --results-directory "TestResults" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput="TestResults/Coverage/coverage" /p:Threshold=35 /p:ThresholdType=line --settings coverlet.runsettings
  ```
  Takes 5 seconds. NEVER CANCEL. Set timeout to 60+ seconds.
- **Publish application**: `dotnet publish Tardis.Web/Tardis.Web.csproj --configuration Release --output /tmp/webapp` -- takes 10 seconds. NEVER CANCEL. Set timeout to 60+ seconds.

### Running the Application
- **Development server**: 
  - From `src/Tardis.Web/`: `dotnet run --configuration Release`
  - Application runs on: http://localhost:5137 (HTTP) and https://localhost:7233 (HTTPS)
  - Use HTTP endpoint for testing to avoid SSL certificate issues
- **Health check endpoint**: `curl http://localhost:5137/health` returns `{"status":"ok"}`
- **Home page**: `curl http://localhost:5137/` returns HTML page with TARDIS theme

## Validation

### CRITICAL: Always validate your changes by running through these complete scenarios:

1. **Build Validation**: 
   - ALWAYS run `dotnet restore` then `dotnet build Tardis.sln --no-restore -warnaserror` before committing
   - Build MUST complete without warnings (treated as errors)
   
2. **Test Validation**:
   - ALWAYS run `dotnet test Tardis.sln --no-build` to ensure all 13 tests pass
   - Check that test summary shows: "total: 13, failed: 0, succeeded: 13, skipped: 0"
   
3. **Application Functionality**:
   - ALWAYS start the application with `dotnet run` from `src/Tardis.Web/`
   - ALWAYS test the health endpoint: `curl http://localhost:5137/health` should return `{"status":"ok"}`
   - ALWAYS test the home page loads: `curl -s -o /dev/null -w "%{http_code}" http://localhost:5137/` should return `200`

4. **Code Coverage**:
   - Coverage threshold is set to 35% line coverage minimum
   - Results saved to `TestResults/Coverage/coverage.cobertura.xml`

## Common Tasks

### Project Structure
```
repo root:
├── .azdo/                      # Azure DevOps CI/CD pipeline
├── .github/workflows/ci.yml    # GitHub Actions CI/CD pipeline  
├── src/
│   ├── Tardis.sln             # Main solution file
│   ├── Tardis.Web/            # Main web application
│   └── Tardis.Web.Tests/      # xUnit test project
├── build-and-test.bat         # Windows batch script (use dotnet commands on Linux)
├── readme.md                  # Project documentation
└── .editorconfig              # StyleCop rule suppressions
```

### Key Files to Check After Changes
- Always check `src/Tardis.Web/Controllers/` after modifying controller logic
- Always check `src/Tardis.Web.Tests/` after adding new features to ensure tests exist
- Always run StyleCop linting when modifying any .cs files
- Configuration files: `appsettings.json`, `appsettings.Development.json`

### Dependencies and Technologies
- **Framework**: .NET 9, ASP.NET Core MVC
- **Frontend**: Bootstrap 5 (included in wwwroot/lib/), Razor views
- **Testing**: xUnit, Moq, Coverlet for coverage
- **Linting**: StyleCop.Analyzers with custom rules in .editorconfig
- **Static Assets**: Bundled and compressed for production builds

### Troubleshooting
- **Build fails on Linux**: The `build-and-test.bat` script only works on Windows. Use the dotnet commands directly.
- **StyleCop warnings**: Check `.editorconfig` for suppressed rules. Most common rules are already suppressed.
- **Port conflicts**: Default ports are 5137 (HTTP) and 7233 (HTTPS). Use HTTP for testing.
- **Coverage threshold failures**: Minimum 35% line coverage required. Add more tests if below threshold.

### CI/CD Pipelines
- **GitHub Actions**: `.github/workflows/ci.yml` - builds on Windows with .NET 9, runs tests with coverage, deploys to Azure
- **Azure DevOps**: `.azdo/simple_ci.yaml` - similar pipeline for Azure DevOps self-hosted agent
- Both pipelines enforce StyleCop linting and code coverage thresholds

### Performance Expectations
- Fresh restore: ~10 seconds
- Clean build: ~12 seconds  
- Incremental build: ~2 seconds
- Test run: ~2 seconds
- Test with coverage: ~5 seconds
- Publish: ~10 seconds
- Application startup: ~2 seconds

### NEVER CANCEL Commands
All dotnet commands may take longer than expected. ALWAYS set generous timeouts:
- Build commands: minimum 60 seconds timeout
- Test commands: minimum 30 seconds timeout  
- Restore commands: minimum 60 seconds timeout
- NEVER cancel builds that appear to hang - they may be processing static assets or running analyzers