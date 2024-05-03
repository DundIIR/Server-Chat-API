# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# COPY FirstChat/bin/Debug/net8.0/ /app
# ENTRYPOINT ["dotnet", "FirstChat.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
#EXPOSE 7116

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY . .
#"FirstChat.csproj"
RUN dotnet build -c Release -o /app/build

#FROM build AS publish
#RUN dotnet build "FirstChat.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/build .

ENTRYPOINT ["dotnet", "FirstChat.dll"]

