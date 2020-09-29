# stage - base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as base

WORKDIR /workspace

COPY . .

RUN dotnet restore
RUN dotnet build --no-restore
RUN dotnet publish --configuration Release --no-restore --output out RVTR.Idaas.Okta.Web/*.csproj

# stage - final
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /workspace

COPY --from=base /workspace/out/ /workspace/

LABEL maintainer="https://github.com/fredbelotte"

CMD [ "dotnet", "RVTR.Idaas.Okta.Web.dll" ]
