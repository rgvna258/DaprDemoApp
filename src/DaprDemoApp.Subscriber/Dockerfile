#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/DaprDemoApp.Subscriber/DaprDemoApp.Subscriber.csproj", "src/DaprDemoApp.Subscriber/"]
COPY ["src/DaprDemoApp.Common/DaprDemoApp.Common.csproj", "src/DaprDemoApp.Common/"]
RUN dotnet restore "src/DaprDemoApp.Subscriber/DaprDemoApp.Subscriber.csproj"
COPY . .
WORKDIR "/src/src/DaprDemoApp.Subscriber"
RUN dotnet build "DaprDemoApp.Subscriber.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DaprDemoApp.Subscriber.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DaprDemoApp.Subscriber.dll"]