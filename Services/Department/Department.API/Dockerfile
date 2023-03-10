#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Department/Department.API/Department.API.csproj", "Services/Department/Department.API/"]
RUN dotnet restore "Services/Department/Department.API/Department.API.csproj"
COPY . .
WORKDIR "/src/Services/Department/Department.API"
RUN dotnet build "Department.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Department.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Department.API.dll"]