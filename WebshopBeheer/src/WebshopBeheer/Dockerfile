FROM microsoft/aspnetcore:1.0
ENTRYPOINT ["dotnet", "WebshopBeheer.dll"]

ENV ASPNETCORE_URLS http://+:5000
ARG SOURCE=./bin/publish

WORKDIR /app
COPY $SOURCE .

EXPOSE 5000