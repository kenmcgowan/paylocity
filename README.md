# Paylocity Pay Period Preview Calculator

This application was written as a code sample for paylocity.com.

## Overview
The application consists of two parts:

 * A UI written  in ReactJS & Redux located in the `/ui` folder.
 * A REST API written in C#, .NET Core, and MS MVC Web API located in the `api` folder.

The configuration files included with the source code should be sufficient to build and run the application without having to adjust any settings.

### Building and testing the api
The API was built using MS Visual Studio 2017 Community Edition. You can either open the solution file with a suitable version of Visual Studio or you can build the solution using the `dotnet` cli. Throughout these instructions, I'll describe how to build, run tests, and run the app using command line tools

```
# Build the solution, then run unit and integration tests.

# First make sure you're in the /api folder.
cd api

# To build the API solution:
dotnet build

# Run unit tests
dotnet test .\Paylocity.Benefits.Registration.Api.Tests\Paylocity.Benefits.Registration.Api.Tests.csproj

# Run integration tests
dotnet test .\Paylocity.Benefits.Registration.Api.IntegrationTests\Paylocity.Benefits.Registration.Api.IntegrationTests.csproj
```

**Note:** While the integration tests are written using the xUnit framework, they have to be run using `dotnet test` instead of `dotnet xunit`. XUnit doesn't yet play nice with some of the dependencies required to run the test HTTP server.

### Building and testing the ui
The ui project was initially created as a `create-react-app` project. To build it, start by running `npm install` from the `/ui` directory.

```
# Install all required packages and also update environment variables used by the app.
cd /ui
npm install
```

From there, you can use the `npm test` command to run tests.

## Running the app

To run the API and UI separately, you'll need two CLI's.

### Start the API
These instructions assume you've already run `npm install` as described above.

```
# Starting from the root project folder, change to the API project folder.
cd api\Paylocity.Benefits.Registration.Api
dotnet run
```

### Start the UI
In a separate CLI, starting again from the root project folder.

```
cd ui
npm start
```