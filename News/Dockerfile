FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY News.csproj /build/

RUN dotnet restore ./build/News.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./News.csproj -c $BUILDCONFIG -o out /p:Version=$VERSION

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "News.dll"] 