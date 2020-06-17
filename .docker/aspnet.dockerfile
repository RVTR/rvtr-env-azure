# stage - base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as base

WORKDIR /workspace

COPY . .

RUN dotnet restore
RUN dotnet build --no-restore
RUN dotnet publish --configuration Release --output out --no-restore <project .csproj>

# stage - final
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

LABEL maintainer="https://github.com/fredbelotte"

WORKDIR /workspace

COPY --from=base /workspace/out/ /workspace/

CMD [ "dotnet", "<project .dll>" ]
