FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file
COPY *.csproj ./

# Restore the project
RUN dotnet restore

# Copy the source code
COPY . ./

# Build the runtime image
RUN dotnet build -c Release -o build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

ARG SniperConfiguration__WalletAddress
ENV SniperConfiguration__WalletAddress=$SniperConfiguration__WalletAddress

ARG SniperConfiguration__WalletPrivateKey
ENV SniperConfiguration__WalletPrivateKey=$SniperConfiguration__WalletPrivateKey

ARG SniperConfiguration__BscscanApiKey
ENV SniperConfiguration__BscscanApiKey=$SniperConfiguration__BscscanApiKey

ARG SniperConfiguration__BscHttpApi
ENV SniperConfiguration__BscHttpApi=$SniperConfiguration__BscHttpApi

ARG SniperConfiguration__BscWsApi
ENV SniperConfiguration__BscWsApi=$SniperConfiguration__BscWsApi

# ARG DOTNET_SNIPER_HONEYPOT_CHECK
# ENV DOTNET_SNIPER_HONEYPOT_CHECK=$DOTNET_SNIPER_HONEYPOT_CHECK

# Set the working directory
WORKDIR /app

# Set the working directory
COPY --from=build /app/build ./

# Set the entry point
ENTRYPOINT ["dotnet", "PancakeTokenSniper.dll"]
