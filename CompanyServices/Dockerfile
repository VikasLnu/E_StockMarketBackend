#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CompanyServices/CompanyAPI.csproj", "CompanyServices/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["DataLayer/DataLayer.csproj", "DataLayer/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["UItility/UItility.csproj", "UItility/"]
RUN dotnet restore "CompanyServices/CompanyAPI.csproj"
COPY . .
WORKDIR "/src/CompanyServices"
RUN dotnet build "CompanyAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompanyAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyAPI.dll"]