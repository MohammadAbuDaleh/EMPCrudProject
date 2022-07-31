#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["EmpCRUD_API/EmpCRUD_API.csproj", "EmpCRUD_API/"]
RUN dotnet restore "EmpCRUD_API/EmpCRUD_API.csproj"
COPY . .
WORKDIR "/src/EmpCRUD_API"
RUN dotnet build "EmpCRUD_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmpCRUD_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmpCRUD_API.dll"]