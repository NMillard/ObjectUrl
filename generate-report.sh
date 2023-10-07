#!/bin/bash

# Restore tools with dotnet tool restore
dotnet tool restore

# Execute tests
dotnet test --settings .runsettings

# Generate report with ReportGenerator
dotnet reportgenerator \
    "-reports:./CodeCoverage/*/coverage.cobertura.xml" \
    "-targetdir:./CodeCoverage/Report" \
    "-reporttypes:Html"