# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and projects
COPY *.sln .
COPY DocumentAccessApproval.WebApi/*.csproj ./DocumentAccessApproval.WebApi/
COPY DocumentAccessApproval.BusinessLogic/*.csproj ./DocumentAccessApproval.BusinessLogic/
COPY DocumentAccessApproval.DataLayer/*.csproj ./DocumentAccessApproval.DataLayer/
COPY DocumentAccessApproval.Domain/*.csproj ./DocumentAccessApproval.Domain/
COPY DocumentAccessApproval.Test/ DocumentAccessApproval.Test/

# Restore dependencies
RUN dotnet restore

# Copy everything
COPY . .

# Build and publish
RUN dotnet publish DocumentAccessApproval.WebApi/DocumentAccessApproval.WebApi.csproj -c Release -o /app/publish

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port the app listens on
EXPOSE 80

ENTRYPOINT ["dotnet", "DocumentAccessApproval.WebApi.dll"]
