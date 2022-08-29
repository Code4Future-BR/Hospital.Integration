FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Hospital.Integration.Api/Hospital.Integration.Api.csproj", "Hospital.Integration.Api/"]
RUN dotnet restore "Hospital.Integration.Api/Hospital.Integration.Api.csproj"
COPY . .
WORKDIR "/src/Hospital.Integration.Api"
RUN dotnet build "Hospital.Integration.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hospital.Integration.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hospital.Integration.Api.dll"]
