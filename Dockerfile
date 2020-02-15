FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /my-cert

COPY ./myserver.cnf .
RUN apk upgrade --update-cache --available
RUN apk add openssl
RUN rm -rf /var/cache/apk/*
RUN touch myserver.key
RUN chmod 600 myserver.key
RUN openssl req -new -config myserver.cnf -keyout myserver.key -out myserver.csr 2>/dev/null
RUN openssl x509 -signkey myserver.key -in myserver.csr -req -days 365 -out myserver.crt 2>/dev/null
RUN openssl pkcs12 -export -out myserver.pfx -inkey ./myserver.key -in ./myserver.crt --password pass:

# Don't use password less certificate in prod ;)

WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./*.sln .
COPY ./MyHub/MyHub.csproj ./MyHub/
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /app/MyHub
RUN dotnet publish --configuration Release --self-contained --runtime linux-musl-x64

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/MyHub/bin/Release/netcoreapp3.1/linux-musl-x64/publish ./
COPY --from=build /my-cert/myserver.pfx ./

# ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 5000
EXPOSE 5001

CMD ["./MyHub"] 