FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder
COPY . /code
WORKDIR /code/src/Cars
RUN dotnet restore && dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY --from=builder /code/src/Cars/publish /app
WORKDIR /app
ENV ASPNETCORE_URLS="http://*:5001"
EXPOSE 5001
RUN apt-get update && apt-get install -y curl
RUN curl https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh > wait_for_it.sh
ENTRYPOINT [ "dotnet", "/app/Cars.dll" ]