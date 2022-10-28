#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MinimalAPISampleProject/MinimalAPISampleProject.csproj", "MinimalAPISampleProject/"]
RUN dotnet restore "MinimalAPISampleProject/MinimalAPISampleProject.csproj"
COPY . .
WORKDIR "/src/MinimalAPISampleProject"
RUN dotnet build "MinimalAPISampleProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalAPISampleProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalAPISampleProject.dll"]