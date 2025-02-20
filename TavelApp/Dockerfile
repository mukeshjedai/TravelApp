FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TravelApp/TravelApp.csproj", "TravelApp/"]
RUN dotnet restore "TravelApp/TravelApp.csproj"
COPY . . 
WORKDIR "/src/TravelApp"
RUN dotnet build "TravelApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravelApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelApp.dll"]
