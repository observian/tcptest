FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /build

COPY ./TcpTest/TcpTest.csproj ./TcpTest/TcpTest.csproj
COPY TcpTest.sln .

RUN dotnet restore TcpTest.sln 
COPY . .

RUN dotnet publish -c Release TcpTest.sln  -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
WORKDIR /app

RUN useradd -ms /bin/bash dotnetuser \
&& chown dotnetuser /app 
USER dotnetuser
COPY --from=build /publish .
CMD ["dotnet", "TcpTest.dll"]