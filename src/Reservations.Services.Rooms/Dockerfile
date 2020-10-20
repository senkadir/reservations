FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

RUN mkdir -p /Reservations.Services.Rooms/
RUN mkdir -p /Reservations.Services.Contracts/
RUN mkdir -p /Reservations.Common/ 

COPY ./Reservations.Services.Rooms/. ./Reservations.Services.Rooms
COPY ./Reservations.Services.Contracts/. ./Reservations.Services.Contracts
COPY ./Reservations.Common/. ./Reservations.Common

RUN dotnet publish ./Reservations.Services.Rooms/ -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT Docker

EXPOSE 5000

ENTRYPOINT ["dotnet","Reservations.Services.Rooms.dll"]
