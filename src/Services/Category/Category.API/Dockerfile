#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Category/Category.API/Category.API.csproj", "Services/Category/Category.API/"]
COPY ["Common/Search/Search.Service/Search.Service.csproj", "Common/Search/Search.Service/"]
COPY ["Common/Core/Core.Service/Core.Service.csproj", "Common/Core/Core.Service/"]
COPY ["Services/Category/Category.Application/Category.Application.csproj", "Services/Category/Category.Application/"]
COPY ["Services/Category/Category.Domain/Category.Domain.csproj", "Services/Category/Category.Domain/"]
COPY ["Services/Category/Category.Infrastructure/Category.Infrastructure.csproj", "Services/Category/Category.Infrastructure/"]
RUN dotnet restore "Services/Category/Category.API/Category.API.csproj"
COPY . .
WORKDIR "/src/Services/Category/Category.API"
RUN dotnet build "Category.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Category.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Category.API.dll"]