#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SDCL_ChatTool/SDCL_ChatTool.csproj", "SDCL_ChatTool/"]
RUN dotnet restore "SDCL_ChatTool/SDCL_ChatTool.csproj"
COPY . .
WORKDIR "/src/SDCL_ChatTool"
RUN dotnet build "SDCL_ChatTool.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SDCL_ChatTool.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SDCL_ChatTool.dll"]