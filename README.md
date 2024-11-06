# AspNetCore.Examples.EntityFrameworkCore

This project serves as an example of how to use and test Entity Framework Core in an ASP.Net Core application.

It makes use of the following technologies and projects:

- [.NET 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- [ASP.NET Core 9.0](https://learn.microsoft.com/aspnet/core/?view=aspnetcore-9.0)
- [SwaggerUI](https://swagger.io/tools/swagger-ui/)
- [ScalaR](https://github.com/ScalaR/ScalaR)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [Docker](https://www.docker.com/)
- [SQL Server](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-bash)
- [XUnit](https://xunit.net/)
- [FluentAssertions](https://fluentassertions.com/)
- [TestContainers](https://testcontainers.com/)
- [Coverlet](https://github.com/coverlet-coverage/coverlet)
- [ReportGenerator](https://reportgenerator.io/)

# How to run
Make sure to have docker running, and run the Docker Compose project from Visual Studio, or from a terminal by executing `docker compose up` in the root folder.

## Call the endpoints via SwaggerUI
Access the web api at http://localhost:54876/swagger and try to call the endpoints for the resources CRUD operations.

## Call the endpoints via ScalaR
Access the web api at http://localhost:54876/scalar/v1 and try to call the endpoints for the resources CRUD operations.

# How to test
Make sure to have docker running, and run the tests from the Test Explorer in Visual Studio, or from a terminal by executing `dotnet test .\AspNetCore.Examples.EntityFrameworkCore.sln` in the root folder.
[More about ASP.NET Core 9.0 integration tests](https://learn.microsoft.com/aspnet/core/test/integration-tests?view=aspnetcore-9.0)

To run tests and generate an html coverage analysis report, run the `coverage_report.ps1` powershell script  in the root folder.
[More about code coverage for unit testing in .NET](https://learn.microsoft.com/dotnet/core/testing/unit-testing-code-coverage)