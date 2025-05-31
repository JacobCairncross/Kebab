FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as copy-stage
WORKDIR /App
COPY --from=build-env /App/out .
# COPY ./Migrations ./Migrations
COPY ./PublicKey .

# TODO: See if we can move these two commands up a stage, ef update is probably all thats needed
# is this even going to do anything? postgres is in another container
# FROM copy-stage AS migration
# RUN dotnet tool install --version 6.0.9 --global dotnet-ef
# ENV PATH="$PATH:/root/.dotnet/tools"
# # ENTRYPOINT dotnet-ef database update --project src/Infrastructure/ --startup-project src/Api/
# RUN dotnet-ef database update
ENTRYPOINT ["dotnet", "kebab.dll"]