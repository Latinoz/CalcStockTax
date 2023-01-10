#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CalcTaxSRV/CalcTaxSRV.csproj", "CalcTaxSRV/"]
RUN dotnet restore "CalcTaxSRV/CalcTaxSRV.csproj"
COPY . .
WORKDIR "/src/CalcTaxSRV"
RUN dotnet build "CalcTaxSRV.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalcTaxSRV.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalcTaxSRV.dll"]